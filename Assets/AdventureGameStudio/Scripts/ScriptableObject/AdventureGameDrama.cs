using Sakuya.UnityUIAnime;
using Sakuya.UnityUIAnime.Define;
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
        //public ADV_BackgroundImageTransition backgroundAnime;
        public Sprite backgroundImage;
        public Texture backgroundRuleImage;
        public float backgroundFadeDuration = 0.8f;
        //public AnimeQueueDefine bgAnimeDefine;

        //图层演出
        public ADV_PerformImageTransition[] layerAnimes;
        public UnityEvent OnEnter;
        public UnityEvent OnExit;
        public ADV_Drama_Composition_Branch[] branchs;
    }

    /// <summary>
    /// 演出用图层
    /// </summary>
    public enum ADV_PerformImageTransitionLayer
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
        Background
    }

    /// <summary>
    /// 演出类型
    /// </summary>
    public enum ADV_LayerTransitionType
    {
        ReplaceExisting,
        UseExisting
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
        public ADV_LayerTransitionType type;
        public Sprite image;
        public Texture ruleImage;
    }

    [System.Serializable]
    public class ADV_PerformImageTransition : ADV_TransitionBase
    {
        public ADV_PerformImageTransitionLayer layer;
        public AnimeQueueSettings animeSettings;
    }

    [System.Serializable]
    public class ADV_BackgroundImageTransition : ADV_TransitionBase
    {
        public AnimeQueueSettings[] anime;
    }

    [System.Serializable]
    public class ADV_Drama_Composition_Branch
    {
        public string branchLabel;
        public AdventureGameDrama branch;
    }
}