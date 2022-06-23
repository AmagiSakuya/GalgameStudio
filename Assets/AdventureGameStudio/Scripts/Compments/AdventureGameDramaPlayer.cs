using Sakuya.UnityUIAnime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Playables;

namespace AdventureGame
{
    public class AdventureGameDramaPlayer : MonoBehaviour
    {
        public AdventureGameDrama enterDrama;
        [ReadOnly] public int currentIndex;

        [Header("UI引用")]
        [Tooltip("对话框背景图片")]
        public Image dialogBackgroundImage;
        [Tooltip("角色名文本框")]
        public TMP_Text characterName;
        [Tooltip("对话文本框")]
        public TMP_Text cotent;

        PlayableDirector m_director;
        public PlayableDirector director
        {
            get
            {
                if (m_director == null) m_director = gameObject.GetComponent<PlayableDirector>();
                return m_director;
            }
        }

        AudioSource m_voicePlayer;
        AudioSource m_bgmPlayer;
        AudioSource m_effectPlayer;
        AudioSource m_systemPlayer;
        AudioSource m_hSceneEffectPlayer;

        //打字机动画控制
        TextMeshPro_UIAnime m_cotentUIAnime;
        float m_textTypeSpeed;

        // Start is called before the first frame update
        private void Awake()
        {
            Init();
        }

        void Start()
        {
            PlayCompistion(enterDrama, currentIndex);
        }

        #region 流程控制
        void Init()
        {
            m_voicePlayer = gameObject.AddComponent<AudioSource>();
            m_bgmPlayer = gameObject.AddComponent<AudioSource>();
            m_effectPlayer = gameObject.AddComponent<AudioSource>();
            m_systemPlayer = gameObject.AddComponent<AudioSource>();
            m_hSceneEffectPlayer = gameObject.AddComponent<AudioSource>();
            m_cotentUIAnime = cotent.GetComponent<TextMeshPro_UIAnime>();
            director.playableAsset = null;
            director.time = 0;
            director.extrapolationMode = DirectorWrapMode.Hold;
            director.playOnAwake = false;
        }

        /// <summary>
        /// 播放对话
        /// </summary>
        void PlayCompistion(AdventureGameDrama drama, int index)
        {
            #region 基础
            //播完对话文字
            if (m_cotentUIAnime.IsPlaying())
            {
                m_cotentUIAnime.Dispose();
                return;
            }
            //播完演出
            if (director.playableAsset != null && director.time < director.playableAsset.duration)
            {
                director.time = director.playableAsset.duration;
                return;
            }

            m_cotentUIAnime.Dispose();
            var m_compisition = drama.compositions[index];
            cotent.text = m_compisition.content;
            characterName.text = m_compisition.characterName;
            m_cotentUIAnime.animeDefine.typeWriterAnimeSettings.duration = m_textTypeSpeed * cotent.text.Length;
            m_cotentUIAnime.animeDefine.typeWriterAnimeSettings.delay = m_compisition.voiceDelay;
            m_cotentUIAnime.Play();

            //语音
            if (m_compisition.voice)
            {
                m_voicePlayer.clip = m_compisition.voice;
                m_voicePlayer.PlayDelayed(m_compisition.voiceDelay);
                //m_voicePlayer.Play();
            }

            //BGM
            if (m_compisition.bgm)
            {
                m_bgmPlayer.clip = m_compisition.bgm;
                m_bgmPlayer.Play();
            }
            #endregion

            #region 演出
            if (m_compisition.perform != null)
            {
                director.playableAsset = m_compisition.perform;
                director.Play();
            }
            #endregion

        }

        #endregion

        #region 应用系统设定
        public void SetSoundSettings(ADVSoundSettings m_soundSettings)
        {
            AudioListener.volume = m_soundSettings.overallVolume;
            m_voicePlayer.volume = m_soundSettings.voiceVolume;
            m_bgmPlayer.volume = m_soundSettings.bgmVolume;
            m_effectPlayer.volume = m_soundSettings.effectVolume;
            m_systemPlayer.volume = m_soundSettings.systemVolume;
            m_hSceneEffectPlayer.volume = m_soundSettings.HSceneEffectVolume;
        }

        public void SetTextSettings(ADVTextSettings m_textSettings)
        {
            characterName.font = m_textSettings.font;
            cotent.font = m_textSettings.font;
            m_textTypeSpeed = Util.Remap(m_textSettings.textTypeWriterSpeed, 0.01f, 1f, 0.05f, 0.001f);
        }
        #endregion

        #region 用户输入
        //下一步
        void OnADVControl_Next()
        {
            if (currentIndex + 1 <= enterDrama.compositions.Count - 1)
            {
                currentIndex++;
                PlayCompistion(enterDrama, currentIndex);
            }
        }

        //上一步
        void OnADVControl_Back()
        {
            if (currentIndex - 1 >= 0)
            {
                currentIndex--;
                PlayCompistion(enterDrama, currentIndex);
            }
        }

        #endregion
    }
}