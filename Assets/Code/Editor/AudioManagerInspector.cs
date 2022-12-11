using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(AudioManager))]
public class AudioManagerInspector : Editor
{
    AudioManager _script;
    int _height = 18;
    int _spacing = 2;
    int _amount = 2;
    int _padding = 10;

    void OnEnable()
    {
    }
    public override void OnInspectorGUI()
    {
        _script = (AudioManager)target;
        DrawDefaultInspector();

        int i = 0;
        int j = 0;
        int width = 100;
        // for(int j = 0; j < _script.SFX.Length/3; j++)
        // {
        while(i < _script.SFX.Length)
        {
            j = 0;
            EditorGUILayout.BeginHorizontal();
            do
            {
                var nameProperty = this.serializedObject.FindProperty("SFX").
                    GetArrayElementAtIndex(i).FindPropertyRelative("ClipName");
                var clipProperty = this.serializedObject.FindProperty("SFX").
                    GetArrayElementAtIndex(i).FindPropertyRelative("Clip");
                var volumeProperty = this.serializedObject.FindProperty("SFX").
                    GetArrayElementAtIndex(i).FindPropertyRelative("Volume");
                
                EditorGUILayout.BeginVertical();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(i.ToString(), GUILayout.Width(20));
                EditorGUILayout.PropertyField(nameProperty, GUIContent.none, GUILayout.Width(75));
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(clipProperty, GUIContent.none, GUILayout.Width(75));
                EditorGUILayout.PropertyField(volumeProperty, GUIContent.none, GUILayout.Width(20));
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();

                EditorGUILayout.Space();
                i++;
                j++;
            }
            while((j+1)*width < EditorGUIUtility.currentViewWidth && i < _script.SFX.Length);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
            EditorGUILayout.Space();

        }
        // }


            GUILayout.FlexibleSpace();

        serializedObject.ApplyModifiedProperties();

    }
}

// var labelRect   = new Rect(_padding     , _padding+(i*(_height+_spacing)*_amount)                     , 20, (_height+_spacing));
// var nameRect    = new Rect(_padding + 25, _padding+(i*(_height+_spacing)*_amount)                     , 75, (_height+_spacing));
// var clipRect    = new Rect(_padding     , _padding+(i*(_height+_spacing)*_amount)+(_height+_spacing)  , 75, (_height+_spacing));
// var volumeRect  = new Rect(_padding + 80, _padding+(i*(_height+_spacing)*_amount)+(_height+_spacing)  , 20, (_height+_spacing));

// EditorGUI.LabelField(labelRect, i.ToString());
// EditorGUI.PropertyField(nameRect, nameProperty, GUIContent.none);
// EditorGUI.PropertyField(clipRect, clipProperty, GUIContent.none);
// EditorGUI.PropertyField(volumeRect, volumeProperty, GUIContent.none);
// EditorGUILayout.LabelField(labelRect, i.ToString());