using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sakuya.UnityUIAnime
{
    [System.Serializable]
    public class BaseAnimeSettings
    {
        public float duration;
        public float delay;
        public AnimationCurve curve;
        [Range(-1, 5)] public int timeDecimalPoint = -1;
    }
}