﻿using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GalgameCharacterDefine))]
public class GalgameCharacterDefineEditor : Editor {
    SerializedProperty m_CharacterName;
    SerializedProperty m_Emojis;
    static bool isElementInEditMode = false;
    SerializedProperty _state;
    GameObject m_baseImg;
    GameObject m_faceImg;
    Vector3 _localPostion;
    Vector3 _localRotation;
    Vector3 _localScale;

    private void OnDisable()
    {
        _state = null;
        SceneView.onSceneGUIDelegate -= OnSceneGUI;
    }

    void OnEnable()
    {
        m_CharacterName = serializedObject.FindProperty("CharacterName");
        m_Emojis = serializedObject.FindProperty("Emojis");
        SceneView.onSceneGUIDelegate += OnSceneGUI;
    }

    GameObject createAdjObj(string name, Sprite sprite, Transform parent = null)
    {
        GameObject _obj = new GameObject();
        if (parent != null)
        {
            _obj.transform.SetParent(parent);
        }
        _obj.name = name;
        _obj.AddComponent<SpriteRenderer>().sprite = sprite;
        _obj.transform.position = Vector3.zero;
        _obj.transform.eulerAngles = Vector3.zero;
        return _obj;
    }

    void setFgimgValue(SerializedProperty state)
    {
        m_faceImg.transform.localPosition = state.FindPropertyRelative("Position").vector3Value;
        m_faceImg.transform.eulerAngles = state.FindPropertyRelative("Rotation").vector3Value;
        Vector3 scale = state.FindPropertyRelative("Scale").vector3Value;
        if (scale == Vector3.zero) scale = new Vector3(1, 1, 1);
        m_faceImg.transform.localScale = scale;
    }

    void toggleLock()
    {
        var type = typeof(EditorWindow).Assembly.GetType("UnityEditor.InspectorWindow");
        var window = EditorWindow.GetWindow(type);
        MethodInfo info = type.GetMethod("FlipLocked", BindingFlags.NonPublic | BindingFlags.Instance);
        info.Invoke(window, null);
    }

    private void OnSceneGUI(SceneView sv)
    {
        if (isElementInEditMode && m_faceImg != null)
        {
            serializedObject.Update();
            //Debug.Log(m_faceImg.transform.localPosition);
            SerializedProperty state = _state;
            //Debug.Log(state);
            saveStatus(state);
            serializedObject.ApplyModifiedProperties();
        }
    }

    void saveStatus(SerializedProperty state)
    {
        if (m_faceImg == null) return;
        state.FindPropertyRelative("Position").vector3Value = m_faceImg.transform.localPosition;
        state.FindPropertyRelative("Rotation").vector3Value = m_faceImg.transform.eulerAngles;
        state.FindPropertyRelative("Scale").vector3Value = m_faceImg.transform.localScale;
    }
    void saveStatus(SerializedProperty state, Vector3 pos,Vector3 ro,Vector3 sca)
    {
        state.FindPropertyRelative("Position").vector3Value = pos;
        state.FindPropertyRelative("Rotation").vector3Value = ro;
        state.FindPropertyRelative("Scale").vector3Value = sca;
    }

    public override void OnInspectorGUI()
    {
        // Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
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
                //toggleLock();
                _state = state;
                _localPostion = state.FindPropertyRelative("Position").vector3Value;
                _localRotation = state.FindPropertyRelative("Rotation").vector3Value;
                _localScale= state.FindPropertyRelative("Scale").vector3Value;

                state.FindPropertyRelative("isEditMode").boolValue = true;
               isElementInEditMode = true;
                string emoji = state.FindPropertyRelative("EmojiName").stringValue;
                m_baseImg = createAdjObj(emoji + "_BaseImg", (Sprite)state.FindPropertyRelative("BaseImg").objectReferenceValue);
                m_faceImg = createAdjObj(emoji + "_FaceImg", (Sprite)state.FindPropertyRelative("FaceImg").objectReferenceValue, m_baseImg.transform);
                setFgimgValue(state);
            }
     
            if (isElementInEditMode && isStateEdit&& GUILayout.Button("Reset"))
            {
                //saveStatus(state);
                //Debug.Log(test);
                saveStatus(state, _localPostion, _localRotation, _localScale);
                //state = _state;
                // Debug.Log(state.FindPropertyRelative("Position").vector3Value);
            }

            if (isElementInEditMode && isStateEdit && m_faceImg != null)
            {
                setFgimgValue(state);
            }

            GUI.backgroundColor = oldGUIColor;

            if (isElementInEditMode && isStateEdit && GUILayout.Button("Exit"))
            {
                _state = null;
                state.FindPropertyRelative("isEditMode").boolValue = false;
                isElementInEditMode = false;
                GameObject.DestroyImmediate(m_baseImg);
            }

            //if (!isElementInEditMode && !isStateEdit && GUILayout.Button("上移"))
            //{
            //    m_Emojis.MoveArrayElement(i, i - 1);
            //}

            //if (!isElementInEditMode && !isStateEdit && GUILayout.Button("下移"))
            //{
            //    m_Emojis.MoveArrayElement(i, i + 1);
            //}

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
       // base.OnInspectorGUI();
    }
}
