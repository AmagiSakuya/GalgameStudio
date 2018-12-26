using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GalgamePartDefine))]
public class GalgamePartDefineEditor : Editor
{
    SerializedProperty m_Actions;
    SerializedProperty m_StartBgm;
    SerializedProperty m_BaseBg;
    SerializedProperty m_GalPerform;
    SerializedProperty m_CharacterNames;

    void OnEnable()
    {
        m_Actions = serializedObject.FindProperty("Actions");
        m_StartBgm = serializedObject.FindProperty("StartBgm");
        m_BaseBg = serializedObject.FindProperty("BaseBg");
        m_GalPerform = serializedObject.FindProperty("GalPerform");
        m_CharacterNames = serializedObject.FindProperty("CharacterNames"); 
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(m_StartBgm,new GUIContent("开始背景音"));
        EditorGUILayout.PropertyField(m_BaseBg, new GUIContent("开始背景图"));
        EditorGUILayout.PropertyField(m_GalPerform, new GUIContent("演出"));

        for (int i = 0; i < m_Actions.arraySize; i++)
        {
            GUILayout.BeginVertical("GroupBox");
            var state = m_Actions.GetArrayElementAtIndex(i);

            EditorGUILayout.PropertyField(state, true);
            GUILayout.BeginHorizontal();
            GUILayout.Space(200);
            if (GUILayout.Button("上移"))
            {
                m_Actions.MoveArrayElement(i, i - 1);
            }

            if (GUILayout.Button("下移"))
            {
                m_Actions.MoveArrayElement(i, i + 1);
            }

            if (GUILayout.Button("删除"))
            {
                m_Actions.DeleteArrayElementAtIndex(i);
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }

        if (GUILayout.Button("新增台词"))
        {
            var index = m_Actions.arraySize;
            m_Actions.InsertArrayElementAtIndex(index);
        }
        serializedObject.ApplyModifiedProperties();
    }
}
