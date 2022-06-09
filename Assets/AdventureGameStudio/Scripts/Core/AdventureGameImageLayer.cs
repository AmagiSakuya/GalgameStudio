using Sakuya.UnityUIAnime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Sakuya.UnityUIAnime.Define;

namespace AdventureGame
{
    public class AdventureGameImageLayer : MonoBehaviour
    {
        public GameObject imagePrefab;
        [ReadOnly] [SerializeField] string m_ADVImageShaderName = "Hidden/ADVGame/Image";
        Queue<Image_Queue_UIAnime> m_imageQueue;

        private void Awake()
        {
            Init();
        }

        void Init()
        {
            m_imageQueue = new Queue<Image_Queue_UIAnime>();
        }

        //对话背景演出
        public void DoBackgroundAnime(ADV_Drama_Composition compistion, Action Callback = null)
        {
            //判断队列数量
            if (m_imageQueue.Count == 0)
            {
                m_imageQueue.Enqueue(CreateImage());
            }
            if (compistion.backgroundImage == null) return;
            Image_Queue_UIAnime m_exist;
            if (!m_imageQueue.TryPeek(out m_exist)) { return; }
            Image_Queue_UIAnime m_newImage = CreateImage();
            m_imageQueue.Enqueue(m_newImage);
            //设置参数
            SetRule(m_newImage, compistion.backgroundRuleImage);
            SetImageSettings_Anime_Queue(m_newImage, compistion.backgroundImage, CreateFadeAnimeQueue(false, compistion.backgroundFadeDuration));
            //fade退出原有
            SetRule(m_exist, null);
            SetImageSettings_FadeOut_Queue(m_exist, compistion.backgroundFadeDuration); ;

            m_exist.OnFadeComplete += () =>
            {
                m_imageQueue.Dequeue();
                Destroy(m_exist.gameObject);
                Callback?.Invoke();
            };
            m_newImage.Play();
            m_exist.Play();
        }

        public Image_Queue_UIAnime Peek()
        {
            return m_imageQueue.Peek();
        }

        /// <summary>
        /// 创建Layer内Image
        /// </summary>
        /// <returns></returns>
        Image_Queue_UIAnime CreateImage()
        {
            GameObject m_image = Instantiate(imagePrefab, gameObject.transform);
            m_image.GetComponent<Image>().material = new Material(Shader.Find(m_ADVImageShaderName));
            return m_image.GetComponent<Image_Queue_UIAnime>();
        }

        /// <summary>
        /// 设置Image 图片 Fade动画序列
        /// </summary>
        void SetImageSettings_Anime_Queue(Image_Queue_UIAnime m_ImageUIAnime, Sprite image, AnimeQueueSettings[] queue)
        {
            m_ImageUIAnime.target.sprite = image;
            m_ImageUIAnime.animeDefine = ScriptableObject.CreateInstance<AnimeQueueDefine>();
            m_ImageUIAnime.animeDefine.animeFadeQueue = queue;
        }

        /// <summary>
        /// 设置FadeOut 动画序列
        /// </summary>
        void SetImageSettings_FadeOut_Queue(Image_Queue_UIAnime m_ImageUIAnime, float fadeDuration = 0.8f)
        {
            m_ImageUIAnime.animeDefine = ScriptableObject.CreateInstance<AnimeQueueDefine>();
            m_ImageUIAnime.animeDefine.animeFadeQueue = CreateFadeAnimeQueue(true, fadeDuration);
        }

        /// <summary>
        /// 设置Shader Rule Image
        /// </summary>
        void SetRule(Image_Queue_UIAnime m_ImageUIAnime, Texture ruleImage)
        {
            m_ImageUIAnime.target.material.SetFloat("_USE_RULE_TEX", ruleImage == null ? 0.0f : 1.0f);
            m_ImageUIAnime.target.material.SetTexture("_RuleTex", ruleImage);
        }

        AnimeQueueSettings[] CreateFadeAnimeQueue(bool isFadeOut = false, float duration = 0.8f)
        {
            AnimeQueueSettings[] m_queue = new AnimeQueueSettings[] { new AnimeQueueSettings() };
            m_queue[0].animeType = AnimeType.ShaderFloatProperty;
            m_queue[0].curve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
            m_queue[0].delay = 0.1f;
            m_queue[0].duration = duration;
            m_queue[0].from = isFadeOut ? Vector4.one : Vector4.zero;
            m_queue[0].to = isFadeOut ? Vector4.zero : Vector4.one;
            m_queue[0].propertyName = "_Progress";
            return m_queue;
        }
    }
}