using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SaveHelper))]
public class SaveHelperInspector : Editor
{
    public override void OnInspectorGUI()
    {
        SaveHelper script = (SaveHelper)target;
        DrawDefaultInspector();


        GUILayout.BeginHorizontal("box");
        if (GUILayout.Button("Set Save Data"))
        {
            script.SetSaveData();
        }
        else if (GUILayout.Button("Reset Save Data"))
        {
            script.ResetSaveData();
        }
        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal("box");
        if (GUILayout.Button("Open Save Data Folder"))
        {
            script.OpenSaveDataFolder();
        }
        else if (GUILayout.Button("Delete Save Data"))
        {
            script.DeleteSaveData();
        }
        GUILayout.EndHorizontal();
    }
    
}

