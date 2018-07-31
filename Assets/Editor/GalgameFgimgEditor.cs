using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GalgameFgimgEditor: EditorWindow
{
    SerializedProperty m_serializedObj;
    GameObject m_baseImg;
    GameObject m_faceImg;
    bool isAdjustMode =false;
    SerializedObject m_serializedObject;
    Vector3 old_Pos;
    Vector3 old_Rotation;
    Vector3 old_Scale;

    public void Show(SerializedProperty serializedObj, SerializedObject serializedObject) {
        m_serializedObj = serializedObj;
        m_serializedObject = serializedObject;
        ChangeSenceToAdjust();
    }

    /// <summary>
    /// 创建一个调节图片物体
    /// </summary>
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

    /// <summary>
    /// 将场景切换成调节状态
    /// </summary>
    void ChangeSenceToAdjust() {
        if (isAdjustMode)return;
        string emoji = m_serializedObj.FindPropertyRelative("EmojiName").stringValue;
        m_baseImg = createAdjObj(emoji + "_BaseImg", (Sprite)m_serializedObj.FindPropertyRelative("BaseImg").objectReferenceValue);
        m_faceImg = createAdjObj(emoji + "_FaceImg", (Sprite)m_serializedObj.FindPropertyRelative("FaceImg").objectReferenceValue, m_baseImg.transform);
        old_Pos = m_serializedObj.FindPropertyRelative("Position").vector3Value;
        old_Rotation = m_serializedObj.FindPropertyRelative("Rotation").vector3Value;
        old_Scale = m_serializedObj.FindPropertyRelative("Scale").vector3Value;
        setValue(old_Pos, old_Rotation, old_Scale);
        isAdjustMode = true;
    }

    void setValue(Vector3 pos,Vector3 rotation,Vector3 scale)
    {
        m_faceImg.transform.localPosition = pos;
        m_faceImg.transform.eulerAngles = rotation;
        if (scale == Vector3.zero) scale = new Vector3(1, 1, 1);
        m_faceImg.transform.localScale = scale;
    }
    private void OnGUI()
    {
        m_serializedObject.Update();
        GUILayout.Space(20);
        Color oldGUIColor = GUI.backgroundColor;
        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("保存", GUILayout.Height(50)))
        {
            m_serializedObj.FindPropertyRelative("Scale").vector3Value = new Vector3(3, 3, 3);
            this.Close();
            m_serializedObject.ApplyModifiedProperties();
        }
        GUI.backgroundColor = oldGUIColor;
        //GUILayout.Space(20);
        //if (GUILayout.Button("重置", GUILayout.Height(50)))
        //{
        //    setValue(old_Pos, old_Rotation, old_Scale);
        //}
        GUILayout.Space(20);
        if (GUILayout.Button("关闭窗口", GUILayout.Height(50)))
        {
            this.Close();
        }
    }

    private void OnDestroy()
    {
            GameObject.DestroyImmediate(m_baseImg);
           // Undo.DestroyObjectImmediate(g);
    }
}
