using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GalgameCharacterDefine))]
public class GalgameCharacterDefineEditor : Editor {
    SerializedProperty m_CharacterName;
    SerializedProperty m_Emojis;
    static bool isElementInEditMode = false;
    static int editIndex = -1;
    static GameObject m_baseImg;
    static GameObject m_faceImg;
    static Vector3 _localPostion;
    static Vector3 _localRotation;
    static Vector3 _localScale;

    private void OnDisable()
    {
        SceneView.onSceneGUIDelegate -= OnSceneGUI;
    }

    void OnEnable()
    {
        m_CharacterName = serializedObject.FindProperty("CharacterName");
        m_Emojis = serializedObject.FindProperty("Emojis");
        SceneView.onSceneGUIDelegate += OnSceneGUI;
    }

    private void OnSceneGUI(SceneView sv)
    {
        if (isElementInEditMode && m_faceImg != null)
        {
            serializedObject.Update();
            if (editIndex == -1) return;
            SerializedProperty state = m_Emojis.GetArrayElementAtIndex(editIndex);
            GalgameUtil.Instance.SaveStatus(state, m_faceImg);
            serializedObject.ApplyModifiedProperties();
        }
    }

    public override void OnInspectorGUI()
    {
       serializedObject.Update();
       EditorGUILayout.PropertyField(m_CharacterName);
        for (int i = 0; i < m_Emojis.arraySize; i++)
        {
            GUILayout.BeginVertical("GroupBox");
            SerializedProperty state = m_Emojis.GetArrayElementAtIndex(i);
            EditorGUILayout.PropertyField(state, true);
            GUILayout.BeginHorizontal();
            GUILayout.Space(120);
            Color oldGUIColor = GUI.backgroundColor;
            GUI.backgroundColor = Color.green;
            bool isStateEdit = state.FindPropertyRelative("isEditMode").boolValue;
            if (!isElementInEditMode && !isStateEdit && GUILayout.Button("直观调整"))
            {
                editIndex = i;
                _localPostion = state.FindPropertyRelative("Position").vector3Value;
                _localRotation = state.FindPropertyRelative("Rotation").vector3Value;
                _localScale= state.FindPropertyRelative("Scale").vector3Value;

                state.FindPropertyRelative("isEditMode").boolValue = true;
               isElementInEditMode = true;
                string emoji = state.FindPropertyRelative("EmojiName").stringValue;
                m_baseImg = GalgameUtil.Instance.CreateGalgameEditorObject(emoji + "_BaseImg", (Sprite)state.FindPropertyRelative("BaseImg").objectReferenceValue);
                m_faceImg = GalgameUtil.Instance.CreateGalgameEditorObject(emoji + "_FaceImg", (Sprite)state.FindPropertyRelative("FaceImg").objectReferenceValue, m_baseImg.transform);
                GalgameUtil.Instance.SetLocalTransform(m_faceImg, state);
            }
     
            if (isElementInEditMode && isStateEdit&& GUILayout.Button("Reset"))
            {
                GalgameUtil.Instance.SaveStatus(state, _localPostion, _localRotation, _localScale);
            }

            if (isElementInEditMode && isStateEdit && m_faceImg != null)
            {
                GalgameUtil.Instance.SetLocalTransform(m_faceImg,state);
            }

            GUI.backgroundColor = oldGUIColor;

            if (isElementInEditMode && isStateEdit && GUILayout.Button("Exit"))
            {
                editIndex = -1;
                state.FindPropertyRelative("isEditMode").boolValue = false;
                isElementInEditMode = false;
                GameObject.DestroyImmediate(m_baseImg);
            }

            if (!isElementInEditMode && !isStateEdit && GUILayout.Button("删除"))
            {
                m_Emojis.DeleteArrayElementAtIndex(i);
            }

            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }

        if (GUILayout.Button("新增表情"))
        {
            var index = m_Emojis.arraySize;
            m_Emojis.InsertArrayElementAtIndex(index);
        }
        serializedObject.ApplyModifiedProperties();
    }
}
