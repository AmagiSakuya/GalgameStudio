using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GalgameCharacterDefine))]
public class GalgameCharacterDefineEditor : Editor {
    SerializedProperty m_CharacterName;
    SerializedProperty m_Emojis;
    static Vector3 _localRotation;
    static Vector3 _localScale;

    void OnEnable()
    {
        m_CharacterName = serializedObject.FindProperty("CharacterName");
        m_Emojis = serializedObject.FindProperty("Emojis");
    }

    public override void OnInspectorGUI()
    {
       serializedObject.Update();
       EditorGUILayout.PropertyField(m_CharacterName, new GUIContent("角色名"));
        for (int i = 0; i < m_Emojis.arraySize; i++)
        {
            GUILayout.BeginVertical("GroupBox");
            SerializedProperty state = m_Emojis.GetArrayElementAtIndex(i);
            EditorGUILayout.PropertyField(state.FindPropertyRelative("EmojiName"), new GUIContent("表情名称"));
            EditorGUILayout.PropertyField(state.FindPropertyRelative("EmojiImg"), new GUIContent("表情图片"));
            Color oldGUIColor = GUI.backgroundColor;
            GUI.backgroundColor = Color.red;

            if (GUILayout.Button("删除表情"))
                if(EditorUtility.DisplayDialog("确定删除表情?（删除后将无法挽回）", "确定删除 "+ m_CharacterName .stringValue+" 的 "+ state.FindPropertyRelative("EmojiName") .stringValue+ " 表情？", "确定", "取消"))
                    m_Emojis.DeleteArrayElementAtIndex(i);
        
            GUI.backgroundColor = oldGUIColor;
            GUILayout.EndVertical();
        }

        if (GUILayout.Button("新增表情"))
         m_Emojis.InsertArrayElementAtIndex(m_Emojis.arraySize);
    
        serializedObject.ApplyModifiedProperties();
    }

}
