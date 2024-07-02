using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using Unity.VisualScripting;



#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DhafinFawwaz.Tweener
{   
    [ExecuteInEditMode]
    public abstract class Tweener : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField] float _onDoneDelay = 0;
        public float OnDoneDelay {get => _onDoneDelay; set => _onDoneDelay = value;}
        [SerializeField] UnityEvent _onTweenDone;
        public UnityEvent OnTweenDone => _onTweenDone;
        public Action OnDone;
        
        [Header("Interrupts")]
        [SerializeField] Tweener[] _otherTweenToStop;
        public Tweener[] OtherTweenToStop => _otherTweenToStop;
        
        [Header("Timing")]
        [SerializeField] protected float _duration = 0.25f;
        public float Duration {get => _duration; set => _duration = value;}
        [SerializeField] Ease.Type _easeType = Ease.Type.Out;
        [SerializeField] Ease.Power _easePower = Ease.Power.Quart;
        Ease.Function _easeFunction;
        
        
        void Awake()
        {
            _easeFunction = Ease.GetEase(_easeType, _easePower);
        }

        void Reset()
        {
            _easeFunction = Ease.GetEase(_easeType, _easePower);
        }


        public void StopOthers()
        {
            foreach (var g in _otherTweenToStop)
                g.Stop();
        }

        protected void StopCoroutineIfNull(Coroutine c)
        {
            if (c != null) StopCoroutine(c);
        }

        public abstract void Stop();

        protected IEnumerator Tween<T>(Action<T> action, T start, T end, float duration, Func<T, T, float, T> lerpFunction)
        {
#if UNITY_EDITOR
            _easeFunction = Ease.GetEase(_easeType, _easePower);
            if(!Application.isPlaying)
            {
                StartTween(action, start, end, duration, lerpFunction);
                yield return null;
            }
            else
            {

#endif
                float startTime = Time.time;
                float t = 0;
                while (t <= 1)
                {
                    t = (Time.time-startTime)/duration;
                    action.Invoke(lerpFunction(start, end, _easeFunction(t)));
                    yield return null;
                }
                action.Invoke(end);

                if(_onDoneDelay > 0) yield return new WaitForSeconds(_onDoneDelay);
                _onTweenDone?.Invoke();
#if UNITY_EDITOR
            }
#endif
        }
        
#if UNITY_EDITOR
        const float _editorLagDelay = 0.4f;
        [HideInInspector] public float EditorProgress { get; private set; } = 0;
        void StartTween<T>(Action<T> action, T start, T end, float duration, Func<T, T, float, T> lerpFunction)
        {
            float startTime = Time.time;
            float t = 0;
            EditorProgress = 0;
            void UpdateTween()
            {
                t = (Time.time-startTime-_editorLagDelay)/duration;
                if(t < 0) return; // handle small lag on inspector when clicking menu
                EditorProgress = t;
                
                if (t <= 1) action.Invoke(lerpFunction(start, end, _easeFunction(t)));
                else
                {
                    action.Invoke(end);
                    EditorProgress = -1;
                    EditorApplication.update -= UpdateTween;
                }
            }
            
            Undo.undoRedoPerformed += () => {
                action.Invoke(start);
                EditorProgress = -1;
                EditorApplication.update -= UpdateTween;
            };

            EditorApplication.update += UpdateTween;
        }

        void ForceRepaint()
        {
            if (!Application.isPlaying)
            {
                UnityEditor.EditorApplication.QueuePlayerLoopUpdate();
                UnityEditor.SceneView.RepaintAll();
                UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
            }
        }
        protected virtual void OnDrawGizmosSelected()
        {
            ForceRepaint();
        }
#endif

    }

#if UNITY_EDITOR

    [CustomEditor(typeof(Tweener), true)]
    public class TweenerEditor : Editor
    {
        bool _advanced = false;

        void OnEnable()
        {
            Tweener t = (Tweener)target;
            if(t.OnDoneDelay > 0 || t.OtherTweenToStop?.Length > 0) _advanced = true;
        }
        public override void OnInspectorGUI()
        {
            Tweener t = (Tweener)target;
            SerializedProperty prop = serializedObject.GetIterator();

            

            // draw progress bar
            if(t.EditorProgress > 0)
            {
                GUILayout.Space(5);
                Rect r = EditorGUILayout.GetControlRect();
                EditorGUI.ProgressBar(r, t.EditorProgress, "Progress");
            }


            // Enter
            prop.NextVisible(true);

            // Skip m_Script
            prop.NextVisible(false);

            // Events
            SerializedProperty onDone = serializedObject.FindProperty(prop.name);
            prop.NextVisible(false);
            SerializedProperty onDoneDelay = serializedObject.FindProperty(prop.name);
            prop.NextVisible(false);

            // Interrupts
            SerializedProperty otherTweenToStop = serializedObject.FindProperty(prop.name);
            prop.NextVisible(false);

            // Timing
            SerializedProperty duration = serializedObject.FindProperty(prop.name);
            prop.NextVisible(false);
            SerializedProperty easeType = serializedObject.FindProperty(prop.name);
            prop.NextVisible(false);
            SerializedProperty easePower = serializedObject.FindProperty(prop.name);

            // Values
			if (prop.NextVisible(true)) {
				do EditorGUILayout.PropertyField(serializedObject.FindProperty(prop.name), true);
				while (prop.NextVisible(false));
			}

            EditorGUILayout.PropertyField(duration, true);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(easeType, new GUIContent("Ease"), true);
            EditorGUILayout.PropertyField(easePower, GUIContent.none, true);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            

            if(_advanced = GUILayout.Toggle(_advanced, "Advanced"))
            {
                EditorGUILayout.PropertyField(otherTweenToStop, true);

                EditorGUILayout.PropertyField(onDone, true);
                EditorGUILayout.PropertyField(onDoneDelay, true);
            }
		
			serializedObject.ApplyModifiedProperties();

            
        }



    }

#endif
}
