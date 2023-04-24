using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class StateMachineEditor : EditorWindow
{
    const string PATHTOCORE = "Base/#SCRIPTNAME#/#SCRIPTNAME#Core.txt";
    const string PATHTOSTATES = "Base/#SCRIPTNAME#/#SCRIPTNAME#States.txt";
    const string PATHTOIDLESTATE = "Base/#SCRIPTNAME#/States/#SCRIPTNAME#IdleState.txt";
    const string PATHTOJUMPSTATE = "Base/#SCRIPTNAME#/States/#SCRIPTNAME#JumpState.txt";
    const string PATHTOTEMPLATESTATE = "Base/#SCRIPTNAME#/States/#SCRIPTNAME##STATENAME#State.txt";

    const int INDENTWIDTH = 8;


    bool _initializationToggle = true;
    bool _newToggle = true;
    bool _updateToggle = true;
    string _path = "";


    string _coreName = "Enemy";

    [System.Serializable] class StateMachineProperties
    {
        public bool Toggle = false;
        public string NewStateName = "";
    }
    List<StateMachineProperties> _newStateName = new List<StateMachineProperties>();

    Vector2 _scrollPos;

    [MenuItem("Window/State Machine Editor")]
    static void Init()
    {
        StateMachineEditor window = (StateMachineEditor)EditorWindow.GetWindow(typeof(StateMachineEditor));
        window.Show();
    }

    void OnGUI()
    {
        _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
        var foldoutStyle = EditorStyles.foldout;
        foldoutStyle.fontStyle = FontStyle.Bold;

        if(_path == "")
        {
            _path = GetCurrentScriptFolderPath();
        }
        _initializationToggle = EditorGUILayout.BeginFoldoutHeaderGroup(_initializationToggle, "Initialization", foldoutStyle);
        EditorGUI.indentLevel++;
        if(_initializationToggle)
        {
            Rect rect = EditorGUILayout.GetControlRect();
            rect.x += INDENTWIDTH*2;
            rect.width -= INDENTWIDTH*2;

            if(GUI.Button(rect, "Select Folder"))
            {
                string defaultPath = GetCurrentScriptFolderPath();
                _path = EditorUtility.OpenFolderPanel("Select Folder", "", defaultPath);
            }

            _path = EditorGUILayout.TextField("Path: ", _path);
        }
        EditorGUI.indentLevel--;
        EditorGUILayout.EndFoldoutHeaderGroup();

        // Check if directory exist
        if (!Directory.Exists(_path))
        {
            EditorGUILayout.HelpBox("Please select a valid directory", MessageType.Error);
            EditorGUILayout.EndScrollView();
            return;
        }
        
        EditorGUILayout.Space(10);
        _newToggle = EditorGUILayout.BeginFoldoutHeaderGroup(_newToggle, "Add New StateMachine", foldoutStyle);
        EditorGUI.indentLevel++;
        if(_newToggle)
        {
            GUILayout.BeginHorizontal();
            _coreName = EditorGUILayout.TextField("Core Name: ", _coreName);
            // Remove all spaces
            _coreName = _coreName.Replace(" ", "");

            // GUI.Label(GUILayoutUtility.GetLastRect(), new GUIContent("", "Please don't include the 'Core' suffix in this field"));
            // GUILayout.Label(GUI.tooltip);
            EditorGUILayout.LabelField("Core");
            GUILayout.EndHorizontal();

            Rect rect = EditorGUILayout.GetControlRect();
            rect.x += INDENTWIDTH*2;
            rect.width -= INDENTWIDTH*2;

            if(_coreName == "")
                GUI.enabled = false;
            if(GUI.Button(rect, "Generate "+_coreName+"Core"))
            {
                GenerateStateMachine(_coreName);
            }
            GUI.enabled = true;
        }
        EditorGUI.indentLevel--;
        EditorGUILayout.EndFoldoutHeaderGroup();


        EditorGUILayout.Space(10);
        _updateToggle = EditorGUILayout.BeginFoldoutHeaderGroup(_updateToggle, "Update Existing StateMachine", foldoutStyle);
        EditorGUI.indentLevel++;
        if(_updateToggle)
        {
            var paths = Directory.GetDirectories(_path).ToList();
            var coreNames = Directory.GetDirectories(_path).Select(d => new DirectoryInfo(d).Name).ToList();

            // Set the size of the list to the number of paths
            if(_newStateName.Count() != paths.Count())
            {
                _newStateName.Clear();
                for(int i = 0; i < paths.Count(); i++)
                {
                    _newStateName.Add(new StateMachineProperties());
                }
            }

            for(int i = 0; i < paths.Count(); i++)
            {
                if(coreNames[i] == "Base")continue;
                EditorGUILayout.Space(5);

                _newStateName[i].Toggle = EditorGUILayout.Foldout(_newStateName[i].Toggle, coreNames[i]+"Core", foldoutStyle);
                if(!_newStateName[i].Toggle)continue;

                EditorGUI.indentLevel++;


                List<string> states = GetAvailableStates(paths[i], coreNames[i]);
                string statesText = "";
                
                for(int j = 0; j < states.Count(); j++)
                {
                    if(j == states.Count()-1)
                        statesText += states[j];
                    else
                        statesText += states[j] + ", ";
                }
                EditorGUILayout.LabelField("States("+states.Count()+"): "+ statesText);

                
                EditorGUILayout.LabelField("New State Name:");

                GUILayout.BeginHorizontal();
                _newStateName[i].NewStateName = EditorGUILayout.TextField(coreNames[i], _newStateName[i].NewStateName);
                // GUI.Label(GUILayoutUtility.GetLastRect(), new GUIContent("", "Please don't include the '"+ coreNames[i] +"' prefix and the 'State' suffix in this field"));
                // GUILayout.Label(GUI.tooltip);

                // Remove all spaces
                _newStateName[i].NewStateName = _newStateName[i].NewStateName.Replace(" ", "");


                EditorGUILayout.LabelField("State");
                GUILayout.EndHorizontal();

                Rect rect = EditorGUILayout.GetControlRect();
                rect.x += INDENTWIDTH*4;
                rect.width -= INDENTWIDTH*4;

                if(_newStateName[i].NewStateName == "")
                    GUI.enabled = false;
                if(GUI.Button(rect, "Generate "+coreNames[i]+_newStateName[i].NewStateName+"State"))
                {
                    GenerateState(paths[i], coreNames[i], _newStateName[i].NewStateName);
                }
                GUI.enabled = true;

                rect = EditorGUILayout.GetControlRect();
                rect.x += INDENTWIDTH*4;
                rect.width -= INDENTWIDTH*4;

                if(GUI.Button(rect, "Update "+coreNames[i]+"States Manually"))
                {
                    UpdateStateMachine(paths[i], coreNames[i]);
                }

                EditorGUI.indentLevel--;
            }
            
        }
        EditorGUI.indentLevel--;
        EditorGUILayout.EndFoldoutHeaderGroup();
        EditorGUILayout.EndScrollView();
    }

    void GenerateStateMachine(string _coreName)
    {
        string coreTemplate = File.ReadAllText(_path+"/"+PATHTOCORE).Replace("#SCRIPTNAME#", _coreName);
        string statesTemplate = File.ReadAllText(_path+"/"+PATHTOSTATES).Replace("#SCRIPTNAME#", _coreName);
        string idleStateTemplate = File.ReadAllText(_path+"/"+PATHTOIDLESTATE).Replace("#SCRIPTNAME#", _coreName);
        string jumpStateTemplate = File.ReadAllText(_path+"/"+PATHTOJUMPSTATE).Replace("#SCRIPTNAME#", _coreName);

        DirectoryInfo folder = Directory.CreateDirectory(_path+"/"+_coreName);
        File.WriteAllText(_path+"/"+_coreName+"/"+_coreName+"Core.cs", coreTemplate);
        File.WriteAllText(_path+"/"+_coreName+"/"+_coreName+"States.cs", statesTemplate);

        DirectoryInfo statesFolder = Directory.CreateDirectory(_path+"/"+_coreName+"/States");
        File.WriteAllText(_path+"/"+_coreName+"/States/"+_coreName+"IdleState.cs", idleStateTemplate);
        File.WriteAllText(_path+"/"+_coreName+"/States/"+_coreName+"JumpState.cs", jumpStateTemplate);
        

        UpdateStateMachine(_path+"/"+_coreName, _coreName);
        AssetDatabase.Refresh();
        Debug.Log("Successfully generating " + _coreName + "Core");
    }

    void UpdateStateMachine(string path, string _coreName)
    {
        Debug.Log("Updating " + _coreName + "Core" + " in " + path);

        List<string> states = GetAvailableStates(path, _coreName);

        string statesTemplate = File.ReadAllText(_path+"/"+PATHTOSTATES).Replace("#SCRIPTNAME#", _coreName);
        
        string statesEnumText = "";
        string statesConstructorText = "";
        string statesSwitchMethods = "";

        for(int i = 0; i < states.Count(); i++)
        {
            statesEnumText += states[i] + ", ";
            statesConstructorText += $"        _states[State.{states[i]}] = new {_coreName}{states[i]}State(_core, this);\n";
            statesSwitchMethods += $"    public BaseState<{_coreName}States> {states[i]}() => _states[State.{states[i]}];\n";
        }

        statesTemplate = statesTemplate.Replace("#STATESENUM#", statesEnumText);
        statesTemplate = statesTemplate.Replace("#STATESCONSTRUCTOR#", statesConstructorText);
        statesTemplate = statesTemplate.Replace("#STATESSWITCHMETHODS#", statesSwitchMethods);

        File.WriteAllText(_path+"/"+_coreName+"/"+_coreName+"States.cs", statesTemplate);
        AssetDatabase.Refresh();
    }

    void GenerateState(string path, string _coreName, string _newStateName)
    {
        Debug.Log("Generating " + _coreName + _newStateName + "State" + " in " + path);

        string stateTemplate = File.ReadAllText(_path+"/"+PATHTOTEMPLATESTATE).Replace("#SCRIPTNAME#", _coreName);
        stateTemplate = stateTemplate.Replace("#STATENAME#", _newStateName);

        DirectoryInfo folder = Directory.CreateDirectory(_path+"/"+_coreName);
        File.WriteAllText(_path+"/"+_coreName+"/States/"+_coreName+_newStateName+"State.cs", stateTemplate);

        UpdateStateMachine(_path+"/"+_coreName, _coreName);
        AssetDatabase.Refresh();
        Debug.Log("Successfully generating " + _coreName + _newStateName + "State");
    }

    List<string> GetAvailableStates(string path, string coreName)
    {
        List<string> states = new List<string>();
        string[] files = Directory.GetFiles(path+"/States");
        for(int i = 0; i < files.Length; i++)
        {
            string fileExtension = Path.GetExtension(files[i]);
            if(fileExtension == ".cs")
            {
                string fileNameNoExtension = Path.GetFileNameWithoutExtension(files[i]);
                fileNameNoExtension = fileNameNoExtension.Replace(".cs", "");
                states.Add(fileNameNoExtension.Replace(coreName, "").Replace("State", ""));
            }
        }
        return states;
    }

    string GetCurrentScriptFolderPath()
    {
        MonoScript script = MonoScript.FromScriptableObject(this);
        string scriptPath = AssetDatabase.GetAssetPath(script);
        scriptPath = scriptPath.Replace("Assets", Application.dataPath);

        return Path.GetDirectoryName(scriptPath);
    }
}