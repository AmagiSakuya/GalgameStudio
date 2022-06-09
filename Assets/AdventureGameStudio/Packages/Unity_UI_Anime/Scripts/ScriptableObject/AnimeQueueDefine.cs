using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sakuya.UnityUIAnime.Define
{
    [CreateAssetMenu(menuName = "Unity UI Anime Define/Anime Queue Define")]
    public class AnimeQueueDefine : ScriptableObject
    {
        [Header("动画队列")]
        [HideInInspector] public AnimeQueueSettings[] animeFadeQueue; // Fade Use 系统保留 不开放
        public AnimeQueueSettings[] animeQueue; // Layer Use
    }
}