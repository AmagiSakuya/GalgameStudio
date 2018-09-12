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

    public GameObject CreateGalgameChararcter(GalgameKeyframe keyframe, Transform parent = null)
    {
        GalgameCharacterEmoji emoji = keyframe.Character.Emojis[keyframe.EmojiSelect];
        GameObject baseImg = CreateGalgameEditorObject(keyframe.Character.CharacterName, emoji.BaseImg);
        SetLocalTransform(baseImg, keyframe);
        GameObject faceImg = CreateGalgameEditorObject(keyframe.Character.CharacterName + "_表情", emoji.FaceImg, baseImg.transform);
        SetLocalTransform(faceImg, emoji);
        return baseImg;
    }

    public void ChangeCharacterEmoji(GameObject target,GalgameCharacterEmoji emoji)
    {
        target.GetComponent<SpriteRenderer>().sprite = emoji.BaseImg;
        GameObject face = target.transform.GetChild(0).gameObject;
        face.GetComponent<SpriteRenderer>().sprite = emoji.FaceImg;
        SetLocalTransform(face, emoji);
    }

    public void SetLocalTransform(GameObject target, SerializedProperty state)
    {
        if (state == null) return;
        SetLocalTransform(target, state.FindPropertyRelative("Position").vector3Value, state.FindPropertyRelative("Rotation").vector3Value, state.FindPropertyRelative("Scale").vector3Value);
    }

    public void SetLocalTransform(GameObject target, GalgameCharacterEmoji emoji)
    {
        if (target == null) return;
        SetLocalTransform(target, emoji.Position, emoji.Rotation, emoji.Scale);
    }

    public void SetLocalTransform(GameObject target, GalgameKeyframe keyframe)
    {
        if (target == null) return;
        SetLocalTransform(target, keyframe.Position, keyframe.Rotation, keyframe.Scale);
    }

    public void SetLocalTransform(GameObject target, Vector3 position, Vector3 rotation, Vector3 scale)
    {
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
