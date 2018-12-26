using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GalgameUtil
{

    public GameObject CreateGalgameEditorObject(string name, Sprite sprite, Transform parent = null, bool isFade = false)
    {
        GameObject obj = new GameObject();
        if (parent != null) obj.transform.SetParent(parent);
        obj.name = name;
        SpriteRenderer _render = obj.AddComponent<SpriteRenderer>();
        if (isFade) _render.color = new Color(1,1,1,0);
        _render.sprite = sprite;
        obj.transform.position = Vector3.zero;
        obj.transform.eulerAngles = Vector3.zero;
        return obj;
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
