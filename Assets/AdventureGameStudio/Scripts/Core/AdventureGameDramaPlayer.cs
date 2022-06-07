using Sakuya.UnityUIAnime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace AdventureGame
{
    public class AdventureGameDramaPlayer : MonoBehaviour
    {
        public AdventureGameDrama enterDrama;
        [ReadOnly] public int currentIndex;
        [Header("UI引用")]
        [Tooltip("背景图片")] 
        public Image backgroundImage;
        [Tooltip("对话框背景图片")] 
        public Image dialogBackgroundImage;
        [Tooltip("角色名文本框")] 
        public TMP_Text characterName;
        [Tooltip("对话文本框")] 
        public TMP_Text cotent;

        AudioSource m_voicePlayer;
        AudioSource m_bgmPlayer;
        AudioSource m_effectPlayer;
        AudioSource m_systemPlayer;
        AudioSource m_hSceneEffectPlayer;

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

        void Init()
        {
            m_voicePlayer = gameObject.AddComponent<AudioSource>();
            m_bgmPlayer = gameObject.AddComponent<AudioSource>();
            m_effectPlayer = gameObject.AddComponent<AudioSource>();
            m_systemPlayer = gameObject.AddComponent<AudioSource>();
            m_hSceneEffectPlayer = gameObject.AddComponent<AudioSource>();
            m_cotentUIAnime = cotent.GetComponent<TextMeshPro_UIAnime>();
        }

        /// <summary>
        /// 播放对话
        /// </summary>
        void PlayCompistion(AdventureGameDrama drama, int index)
        {
            //对话文字
            if (m_cotentUIAnime.IsPlaying())
            {
                m_cotentUIAnime.Dispose();
                return;
            }
            m_cotentUIAnime.Dispose();
            var m_compisition = drama.compositions[index];
            cotent.text = m_compisition.content;
            characterName.text = m_compisition.characterName;
            m_cotentUIAnime.animeDefine.typeWriterAnimeSettings.duration = m_textTypeSpeed * cotent.text.Length;
            m_cotentUIAnime.Play();

            //语言
            if (m_compisition.voice)
            {
                m_voicePlayer.clip = m_compisition.voice;
                m_voicePlayer.Play();
            }

            //BGM
            if (m_compisition.bgm)
            {
                m_bgmPlayer.clip = m_compisition.bgm;
                m_bgmPlayer.Play();
            }

            //背景
            if (m_compisition.background)
            {
                backgroundImage.color = Color.white;
                backgroundImage.sprite = m_compisition.background;
            }
        }

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

        //public void SetDefine(GameDialogStyleDefine value, TMP_FontAsset font = null)
        //{
        //    //设置背景图片
        //    dialogBackgroundImage.sprite = value.backgroundImage;
        //    //设置文本颜色
        //    SetADVTextSettings(characterName, value.charaNameTextSettings, font);
        //    SetADVTextSettings(cotent, value.contentTextSettings, font);
        //}

        //public void SetADVTextSettings(TMP_Text m_text, ADVDramaTextSettings settings, TMP_FontAsset font = null)
        //{
        //    m_text.color = settings.color;
        //    m_text.fontSize = settings.fontSize;
        //    if (font != null)
        //    {
        //        m_text.font = font;
        //    }
        //    m_text.fontSharedMaterial.SetColor("_OutlineColor", settings.outLineColor);
        //    m_text.fontSharedMaterial.SetFloat("_OutlineWidth", settings.outLineWidth);
        //    m_text.fontSharedMaterial.SetFloat("_FaceDilate", settings.outLineWidth);
        //}

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
    }
}