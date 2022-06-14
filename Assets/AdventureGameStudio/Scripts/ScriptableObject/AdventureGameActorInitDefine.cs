using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureGame
{
    [CreateAssetMenu(menuName = "ADV Studio/Anime/Actor Init Define")]
    public class AdventureGameActorInitDefine : ScriptableObject
    {
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 scale = Vector3.one;
    }
}