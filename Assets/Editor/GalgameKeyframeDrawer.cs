using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(GalgameKeyframe))]
public class GalgameKeyframeDrawer : PropertyDrawer
{
    SerializedProperty _state;
    GameObject m_baseImg;
    GameObject m_faceImg;
    Vector3 _localPostion;
    Vector3 _localRotation;
    Vector3 _localScale;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        position.height = 16f;
        property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label, true);
        if (property.isExpanded)
        {
            DrawProperty(position, property);
        }
        EditorGUI.EndProperty();
    }

    private static void DrawProperty(Rect position, SerializedProperty property)
    {
        
        //EditorGUI.indentLevel += 1;
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
            index = EditorGUI.Popup(new Rect(position.x, position.y, 200, 16), index, showTxt.ToArray());
            property.FindPropertyRelative("EmojiSelect").intValue = index;
            Color oldGUIColor = GUI.backgroundColor;
            GUI.backgroundColor = Color.green;
            if (GUI.Button(new Rect(position.x+210, position.y, position.xMax - 250, 16), "直观调整"))
            {
                Debug.Log("功能建设中");
            }
            GUI.backgroundColor = oldGUIColor;
            position.y += 20;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("Position"));
            position.y += 20;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("Rotation"));
            position.y += 20;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("Scale"));
            position.y += 20;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("TimeAxis"));
            position.y += 20;
        }
    }


    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return GetPropertyHeight(property);
    }

    public static float GetPropertyHeight(SerializedProperty property)
    {
        var h = 20;
        if (property.isExpanded)
        {
            h += 120;
        }
        return h;
    }
}
