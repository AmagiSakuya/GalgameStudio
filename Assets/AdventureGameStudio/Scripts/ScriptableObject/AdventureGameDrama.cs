using Sakuya.UnityUIAnime;
using Sakuya.UnityUIAnime.Define;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Timeline;

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
        public float voiceDelay;
        public AudioClip bgm;
        public TimelineAsset perform;

        //图层演出
        //public ADV_PerformImageTransition[] layerAnimes;
        public UnityEvent OnEnter;
        public UnityEvent OnExit;
        public ADV_Drama_Composition_Branch[] branchs;
    }

    [System.Serializable]
    public class ADV_Drama_Composition_Branch
    {
        public string branchLabel;
        public AdventureGameDrama branch;
    }
}