using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureGame
{
    [CreateAssetMenu(menuName = "ADV Studio/Anime/Actor Transform Presets")]
    public class AdventureGameActorTransformPresets : ScriptableObject
    {
        public Vector2 position;
        public Vector2 rotation;
        public Vector2 scale = Vector2.one;
    }
}