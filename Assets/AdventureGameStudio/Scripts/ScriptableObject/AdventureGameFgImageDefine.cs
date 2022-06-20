using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureGame
{
    [CreateAssetMenu(menuName = "ADV Studio/FgImage Define")]
    public class AdventureGameFgImageDefine : ScriptableObject
    {
        public Vector2 boundingBoxSize;
        [NonReorderable]
        public AdventureGameFgImageBodyGroup[] bodyGroup;
        [NonReorderable]
        public AdventureGameFgImageFaceGroup[] faceGroup;
        [NonReorderable]
        public AdventureGameFgImageAppendFace[] appendFace;
    }

    [System.Serializable]
    public class AdventureGameFgImage
    {
        public string imageName;
        public Sprite image;
        public Vector2 offset;
    }

    [System.Serializable]
    public class AdventureGameFgImageAppendFace : AdventureGameFgImage
    {
        public bool show;
    }

    [System.Serializable]
    public class AdventureGameFgImageBodyGroup
    {
        public string bodyGroupName;
        [NonReorderable]
        public AdventureGameFgImage[] bodys;
    }

    [System.Serializable]
    public class AdventureGameFgImageFaceGroup
    {
        public string relativeBodyGroup;
        [NonReorderable]
        public AdventureGameFgImage[] faces;
    }

}