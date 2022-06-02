using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace AdventureGame
{
    [System.Serializable]
    public class ADVTextSettings
    {
        public TMP_FontAsset font;
        public TMP_FontAsset[] fonts;
        [Range(0.01f, 1.0f)] public float textTypeWriterSpeed = 0.4f;
        public bool skipUnreadedText = false;
    }

    [System.Serializable]
    public class ADVDramaTextSettings
    {
        public Color color = Color.white;
        public float fontSize = 25;
        [Range(0, 1)] public float outLineWidth = 0;
        public Color outLineColor;
    }
}