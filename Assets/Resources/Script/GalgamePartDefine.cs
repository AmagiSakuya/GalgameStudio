using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GalgamePartDefine :  ScriptableObject
{
    public AudioClip StartBgm;
    public Sprite BaseBg;
    public List<GalgameAction> Actions = new List<GalgameAction>();

    [MenuItem("Assets/Create/Galgame Part 配置文件")]
    static void CreateGalgamePartDenfine()
    {
        ScriptableObjectUtility.CreateAsset<GalgamePartDefine>();
    }
}