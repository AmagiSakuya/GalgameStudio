using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GalgameCharacterDefine : ScriptableObject
{
    public string CharacterName;
    public List<GalgameCharacterEmoji> Emojis = new List<GalgameCharacterEmoji>();

    [MenuItem("Assets/Create/Galgame 角色 配置文件")]
    static void CreateGalgamePartDenfine()
    {
        ScriptableObjectUtility.CreateAsset<GalgameCharacterDefine>();
    }
}
