using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(GalgameKeyframe))]
public class GalgameKeyframeDrawer : PropertyDrawer
{
    static bool isPerformInEditMode = false;
    SerializedProperty _state;
    GameObject m_baseImg;
    GameObject m_faceImg;
    Vector3 _localPostion;
    Vector3 _localRotation;
    Vector3 _localScale;

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
            EditorGUI.PropertyField(position, property.FindPropertyRelative("TimeAxis"));
            position.y += 20;
            Color oldGUIColor = GUI.backgroundColor;
            GUI.backgroundColor = Color.green;
            if (GUILayout.Button("直观调整"))
            {
                Debug.Log("功能建设中");
            }
            GUI.backgroundColor = oldGUIColor;
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
