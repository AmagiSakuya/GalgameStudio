﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(GalgameKeyframe))]
public class GalgameKeyframeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        position.height = 16f;
       // position.x = 36f;
        EditorGUI.BeginProperty(position, label, property);
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
        SerializedProperty m_Chara = property.FindPropertyRelative("Character");
        if (m_Chara.objectReferenceValue != null)
        {
            GalgameCharacterDefine m_obj = (GalgameCharacterDefine)m_Chara.objectReferenceValue;
            position.y += 20;
            List<string> showTxt = new List<string>();
            for(int i = 0;i< m_obj.Emojis.Count; i++)
            {
                showTxt.Add(m_obj.Emojis[i].EmojiName);
            }
            int index = property.FindPropertyRelative("EmojiSelect").intValue;
            index = EditorGUI.Popup(position, index, showTxt.ToArray());
            property.FindPropertyRelative("EmojiSelect").intValue = index;
            position.y += 20;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("Position"));
            position.y += 20;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("Rotation"));
            position.y += 20;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("Scale"));
            position.y += 20;
        }
    }


    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return GetPropertyHeight(property);
    }

    public static float GetPropertyHeight(SerializedProperty property)
    {
        var h = 50;
        return h;
    }
}
