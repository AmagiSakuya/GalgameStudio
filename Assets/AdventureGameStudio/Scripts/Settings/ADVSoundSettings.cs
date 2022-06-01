using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureGame
{
    [System.Serializable]
    public class ADVSoundSettings
    {
        [Range(0, 1)] public float overallVolume = 1.0f;
        [Range(0, 1)] public float bgmVolume = 0.5f;
        [Range(0, 1)] public float voiceVolume = 1.0f;
        [Range(0, 1)] public float effectVolume = 0.5f;
        [Range(0, 1)] public float systemVolume = 0.5f;
        [Range(0, 1)] public float movieVolume = 0.6f;
        [Range(0, 1)] public float HSceneEffectVolume = 0.7f;
    }
}