using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureGame
{
    [CreateAssetMenu(menuName = "ADV Studio/Anime/Anime Queue Define")]
    public class AdventureLayerAnimeInitDefine : ScriptableObject
    {
        public Vector3 position;
        public Vector3 scale = Vector3.zero;
    }
}