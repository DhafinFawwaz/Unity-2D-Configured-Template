using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(AudioManager.Sound))]
public class SFXDrawer : PropertyDrawer
{
    int _height = 18;
    int _spacing = 2;
    int _amount = 2;
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return ((_height+_spacing)*_amount + _spacing);
    }
    
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        
        EditorGUI.BeginProperty(position, label, property);
        // position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;
        
        // Calculate rects
        var labelRect   = new Rect(position.x     , position.y                     , 20, position.height/2-_spacing);
        var nameRect    = new Rect(position.x + 25, position.y                     , 75, position.height/2-_spacing);
        var clipRect    = new Rect(position.x     , position.y+(_height+_spacing), 75, position.height/2-_spacing);
        var volumeRect  = new Rect(position.x + 80, position.y+(_height+_spacing), 20, position.height/2-_spacing);


        EditorGUI.LabelField(labelRect, label.ToString().Remove(0, 8));
        EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("ClipName"), GUIContent.none);
        EditorGUI.PropertyField(clipRect, property.FindPropertyRelative("Clip"), GUIContent.none);
        EditorGUI.PropertyField(volumeRect, property.FindPropertyRelative("Volume"), GUIContent.none);


        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }


    
}

