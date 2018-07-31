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
        EditorGUI.PropertyField(position, property.FindPropertyRelative("Character"));
        position.y += 20;
        EditorGUI.PropertyField(position, property.FindPropertyRelative("Serifu"));
        position.y += 20;
        EditorGUI.PropertyField(position, property.FindPropertyRelative("Voice"));
        position.y += 20;
        EditorGUI.PropertyField(position, property.FindPropertyRelative("ChangeBgm"));
        position.y += 20;
        EditorGUI.PropertyField(position, property.FindPropertyRelative("ChangeBg"));
        position.y += 20;
        EditorGUI.PropertyField(position, property.FindPropertyRelative("Keyframe"),true);
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
            h += 120;
            var m_Keyframe = property.FindPropertyRelative("Keyframe");
            if (m_Keyframe.isExpanded)
            {
                h += 20;
                //Debug.Log(property.FindPropertyRelative("Keyframe").arraySize);
                for (int i = 0; i < m_Keyframe.arraySize; i++)
                {
                    h += 20;
                    SerializedProperty state = m_Keyframe.GetArrayElementAtIndex(i);
                    if (state.isExpanded)
                    {
                        h += 120;
                    }
                }

            }
        }
        return h;
    }
}
