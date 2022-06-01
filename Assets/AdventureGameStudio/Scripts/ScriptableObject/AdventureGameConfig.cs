using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureGame
{
    [CreateAssetMenu(menuName = "ADV Studio/GameConfig")]
    public class AdventureGameConfig : ScriptableObject
    {
        public ADVSystemSettings systemSettings;
        public ADVTextSettings textSettings;
        public ADVSoundSettings soundSettings;

        public Action Apply;
        public void OnValidate()
        {
            Apply?.Invoke();
        }
    }
}