using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GalgameUtil
{

    public GameObject CreateGalgameEditorObject(string name, Sprite sprite, Transform parent = null)
    {
        GameObject obj = new GameObject();
        if (parent != null) obj.transform.SetParent(parent);
        obj.name = name;
        obj.AddComponent<SpriteRenderer>().sprite = sprite;
        obj.transform.position = Vector3.zero;
        obj.transform.eulerAngles = Vector3.zero;
        return obj;
    }

    public void SetLocalTransform(GameObject target, SerializedProperty state)
    {
        if (state == null) return;
        target.transform.localPosition = state.FindPropertyRelative("Position").vector3Value;
        target.transform.eulerAngles = state.FindPropertyRelative("Rotation").vector3Value;
        Vector3 scale = state.FindPropertyRelative("Scale").vector3Value;
        if (scale == Vector3.zero) scale = new Vector3(1, 1, 1);
        target.transform.localScale = scale;
    }

    public void SetLocalTransform(GameObject target, Vector3 position, Vector3 rotation, Vector3 scale)
    {
        if (target == null || position == null || rotation == null || scale == null) return;
        target.transform.localPosition = position;
        target.transform.eulerAngles = rotation;
        if (scale == Vector3.zero) scale = new Vector3(1, 1, 1);
        target.transform.localScale = scale;
    }

    public void SaveStatus(SerializedProperty state, GameObject target)
    {
        if (target == null || state == null) return;
        state.FindPropertyRelative("Position").vector3Value = target.transform.localPosition;
        state.FindPropertyRelative("Rotation").vector3Value = target.transform.eulerAngles;
        state.FindPropertyRelative("Scale").vector3Value = target.transform.localScale;
    }

    public void SaveStatus(SerializedProperty state, Vector3 position, Vector3 rotation, Vector3 scale)
    {
        state.FindPropertyRelative("Position").vector3Value = position;
        state.FindPropertyRelative("Rotation").vector3Value = rotation;
        state.FindPropertyRelative("Scale").vector3Value = scale;
    }

    //单例模式
    private static GalgameUtil _instance = null;

    private GalgameUtil()
    {

    }

    public static GalgameUtil Instance
    {
        get { return _instance ?? (_instance = new GalgameUtil()); }
    }
}
