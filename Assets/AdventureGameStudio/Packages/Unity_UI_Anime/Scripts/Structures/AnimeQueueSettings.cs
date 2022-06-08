using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sakuya.UnityUIAnime
{
    [System.Serializable]
    public class AnimeQueueSettings : Vector4AnimeSettings
    {
        public string propertyName;
        public AnimeType animeType;
        public bool relative;
    }

    public enum AnimeType
    {
        Position,
        Rotation,
        Scale,
        ShaderFloatProperty
    }
}