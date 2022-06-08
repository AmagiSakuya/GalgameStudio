using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sakuya.UnityUIAnime.Define
{
    [CreateAssetMenu(menuName = "Unity UI Anime Define/Anime Queue Define")]
    public class AnimeQueueDefine : ScriptableObject
    {
        [Header("动画队列")]
        public AnimeQueueSettings[] animeQueue;
    }
}