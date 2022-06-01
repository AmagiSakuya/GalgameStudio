using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sakuya.UnityUIAnime.Define
{
    [CreateAssetMenu(menuName = "Unity UI Anime Define/RectTransform Anime Define")]
    public class RectTransform_UIAnime_Define : ScriptableObject
    {
        [Header("位移")]
        public Vector3AnimeSettings[] positionAnimeQueue;
        public bool relativePos;
        [Header("旋转")]
        public Vector3AnimeSettings[] rotationAnimeQueue;
        public bool relativeRotation;
        [Header("缩放")]
        public Vector3AnimeSettings[] scaleAnimeQueue;
        public bool relativeScale;
    }
}