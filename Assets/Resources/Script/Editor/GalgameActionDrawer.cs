using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(GalgameAction))]
public class GalgameActionDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        position.height = 16f;
        position.x = 36f; 
        EditorGUI.BeginProperty(position, label, property);
        label.text = property.FindPropertyRelative("Serifu").stringValue;
        property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label, true);
        if (property.isExpanded)
        { 
            DrawProperty(position, property);
        }
        EditorGUI.EndProperty();
    }

    private static void DrawProperty(Rect position, SerializedProperty property)
    {
        EditorGUI.indentLevel += 1;
        position.y += 20;
        EditorGUI.PropertyField(position, property.FindPropertyRelative("ShowName"),new GUIContent("显示名称"));
        //EditorGUILayout.Popup
        position.y += 20;
        EditorGUI.PropertyField(position, property.FindPropertyRelative("Serifu"), new GUIContent("台词"));
        position.y += 20;
        EditorGUI.PropertyField(position, property.FindPropertyRelative("Voice"), new GUIContent("语音"));
        position.y += 20;
        EditorGUI.PropertyField(position, property.FindPropertyRelative("ChangeBgm"), new GUIContent("换BGM"));
        position.y += 20;
        EditorGUI.PropertyField(position, property.FindPropertyRelative("ChangeBg"), new GUIContent("换背景"));
        position.y += 20;
        EditorGUI.PropertyField(position, property.FindPropertyRelative("PerformMark"), new GUIContent("演出序号"));
    }


    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return GetPropertyHeight(property);
    }

    public static float GetPropertyHeight(SerializedProperty property)
    {
        var h = 16;
        if (property.isExpanded) h += 120;
        return h;
    }
}
