using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DhafinFawwaz.Spawner
{
    public abstract class Spawner : MonoBehaviour
    {
        [Tooltip("The prefab to spawn")]
        [SerializeField] protected GameObject _prefab;

        [Tooltip("The amount of objects to spawn")]
        [SerializeField] protected int _amount = 16;
        public int Amount { get => _amount; set => _amount = value; }

        /// <summary>
        /// Spawns the object
        /// </summary>
        /// <returns></returns>
        public virtual List<GameObject> Spawn()
        {
            List<GameObject> result = new List<GameObject>();
            for(int i = 0; i < _amount; i++)
            {
                Vector3 spawnPosition = GetRandomPosition();
                GameObject spawnedObject = Instantiate(_prefab, transform.position + spawnPosition, Quaternion.identity);
                result.Add(spawnedObject);
#if UNITY_EDITOR
                _editorSpawnedObjects.Add(spawnedObject);
#endif
            }
            return result;
        }

        public abstract Vector3 GetRandomPosition();

#if UNITY_EDITOR

        [Header("Editor Only")]
        [SerializeField] protected bool _drawGizmos = true;
        [SerializeField] protected List<GameObject> _editorSpawnedObjects = new List<GameObject>();
        public List<GameObject> EditorSpawnedObjects => _editorSpawnedObjects;
        protected List<Vector3> _predictedPositions = new List<Vector3>();
        protected virtual void RefreshPredictedPositions()
        {
            _predictedPositions.Clear();
            for(int i = 0; i < _amount; i++)
            {
                _predictedPositions.Add(GetRandomPosition());
            }
        }

        void OnValidate()
        {
            RefreshPredictedPositions();
        }

        public void DestroyAllEditorSpawnedObjects()
        {
            foreach(GameObject go in _editorSpawnedObjects)
            {
                DestroyImmediate(go);
            }
            _editorSpawnedObjects.Clear();
        }
        public virtual void Reposition()
        {
            foreach(GameObject child in _editorSpawnedObjects)
            {
                Vector3 spawnPosition = GetRandomPosition();
                child.transform.position = transform.position + spawnPosition;
            }
        }
#endif
    }

#if UNITY_EDITOR
[UnityEditor.CustomEditor(typeof(Spawner), true)]
public class SpawnerEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Spawner spawner = (Spawner)target;
        if(GUILayout.Button("Spawn " + spawner.Amount + " Objects"))
        {
            spawner.Spawn();
        }
        if(GUILayout.Button("Reposition"))
        {
            spawner.Reposition();
        }
        if(GUILayout.Button("Destroy All " + spawner.EditorSpawnedObjects.Count + " Editor Spawned Objects"))
        {
            spawner.DestroyAllEditorSpawnedObjects();
        }
    }
}
#endif

}
