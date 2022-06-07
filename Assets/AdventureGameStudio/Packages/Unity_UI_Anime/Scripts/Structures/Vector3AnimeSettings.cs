using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sakuya.UnityUIAnime
{
    [System.Serializable]
    public class Vector3AnimeSettings : BaseAnimeSettings
    {
        public Vector3 from;
        public Vector3 to;
    }

    [System.Serializable]
    public class Vector4AnimeSettings : BaseAnimeSettings
    {
        public Vector4 from;
        public Vector4 to;
    }
}