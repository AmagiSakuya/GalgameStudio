using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(GalgameCharacterEmoji))]
public class GalgameCharacterEmojiDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        position.height = 16f;
        position.x = 36f; 
        EditorGUI.BeginProperty(position, label, property);
        label.text = property.FindPropertyRelative("EmojiName").stringValue;
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
        EditorGUI.PropertyField(position, property.FindPropertyRelative("EmojiName"));
        position.y += 20;
        EditorGUI.PropertyField(position, property.FindPropertyRelative("BaseImg"));
        position.y += 20;
        EditorGUI.PropertyField(position, property.FindPropertyRelative("FaceImg"));
        position.y += 20;
        EditorGUI.PropertyField(position, property.FindPropertyRelative("Position"));
        position.y += 20;
        EditorGUI.PropertyField(position, property.FindPropertyRelative("Rotation"));
        position.y += 20;
        EditorGUI.PropertyField(position, property.FindPropertyRelative("Scale"));
    }


    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return GetPropertyHeight(property);
    }

    public static float GetPropertyHeight(SerializedProperty property)
    {
        var h = 16;
        if (property.isExpanded)
        {
            h += 140;
        }
        return h;
    }
}
