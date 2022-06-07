using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AdventureGame
{
    [CreateAssetMenu(menuName = "ADV Studio/AdventureGameDrama")]
    public class AdventureGameDrama : ScriptableObject
    {
        public string m_name;
        public List<ADV_Drama_Composition> compositions = new List<ADV_Drama_Composition>();
        public AdventureGameDrama nextDrama;
        public UnityEvent OnEnter;
        public UnityEvent OnExit;
        public ADV_Drama_Composition m_selectedCompistion;
    }

    [System.Serializable]
    public class ADV_Drama_Composition
    {
        //基础
        public string characterName;
        public string content;
        public AudioClip voice;
        public AudioClip bgm;
        //背景
        public Sprite background;
        public Texture backgroundRuleImage;
        public float backgroundFadeDuration = -1.0f;
        public ADV_ImageTransition[] layerAnimes;

        //图层演出
        public UnityEvent OnEnter;
        public UnityEvent OnExit;
        public ADV_Drama_Composition_Branch[] branchs;
    }

    public enum ADV_ImageTransitionLayer
    {
        Layer1,
        Layer2,
        Layer3,
        Layer4,
        Layer5,
        Layer6,
        Layer7,
        Layer8,
        Layer9,
        Layer10
    }

    public enum ADV_ImageTransitionPos
    {
        Center,
        Left,
        Right
    }

    public enum ADV_ImageTransitionScale
    {
        Normal,
        VeryFar,
        Far,
        Near,
        VeryNear
    }

    [System.Serializable]
    public class ADV_TransitionBase
    {
        public Sprite image;
        public Texture ruleImage;
        public float duration = -1.0f;
    }

    [System.Serializable]
    public class ADV_ImageTransition : ADV_TransitionBase
    {
        public ADV_ImageTransitionLayer layer;
        public ADV_ImageTransitionPos pos;
        public Vector2 posOffset;
        public ADV_ImageTransitionScale scale;
        public Vector2 scaleOffset;
    }

    [System.Serializable]
    public class ADV_Drama_Composition_Branch
    {
        public string branchLabel;
        public AdventureGameDrama branch;
    }
}