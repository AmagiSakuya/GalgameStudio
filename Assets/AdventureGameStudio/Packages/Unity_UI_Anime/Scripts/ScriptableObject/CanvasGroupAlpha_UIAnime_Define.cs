using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sakuya.UnityUIAnime.Define
{
    [CreateAssetMenu(menuName = "Unity UI Anime Define/CanvasGroup Alpha Anime Define")]
    public class CanvasGroupAlpha_UIAnime_Define : ScriptableObject
    {
        [Header("透明度")]
        public FloatAnimeSettings[] alphaAnimeQueue;
    }
}