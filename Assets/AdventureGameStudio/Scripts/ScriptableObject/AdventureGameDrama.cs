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
        [HideInInspector] public ADV_Drama_Composition m_selectedCompistion;
    }

    [System.Serializable]
    public class ADV_Drama_Composition
    {
        public string characterName;
        public string content;
        public AudioClip voice;
        public AudioClip bgm;
        public Sprite background;
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