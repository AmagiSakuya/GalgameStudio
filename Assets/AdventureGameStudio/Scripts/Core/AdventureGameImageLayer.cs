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


        public void PlayerLayers(ADV_PerformImageTransition[] settings)
        {
            if (settings.Length <= 0) return;
            PlayLayer(settings, 0);
        }

        void PlayLayer(ADV_PerformImageTransition[] settings, int index, Action Callback = null)
        {
            PlayLayer(settings[index], () =>
            {
                PlayLayerCallback(settings, index);
            });
        }

        void PlayLayerCallback(ADV_PerformImageTransition[] settings, int index)
        {
            if (index + 1 < settings.Length)
            {
                PlayLayer(settings, index + 1);
            }
        }

        /// <summary>
        /// 图层演出
        /// </summary>
        public void PlayLayer(ADV_PerformImageTransition settings, Action Callback = null)
        {
            Image_Queue_UIAnime m_exist = GetExist();
            if (settings.type == ADV_LayerTransitionType.ReplaceExisting)
            {
                if (settings.image == null)
                {
                    settings.image = m_exist.target.GetComponent<Image>().sprite;
                }

                //如果是新建 Dofade操作
                Image_Queue_UIAnime m_newImage = CreateAndSetFadeAnime(m_exist, settings.image, settings.ruleImage, settings.initDuration, settings.initDelay, Callback);
                //设置新图层位置
                m_newImage.target.SetNativeSize();
                if (settings.initDefine)
                {
                    m_newImage.target.GetComponent<RectTransform>().localPosition = settings.initDefine.position;
                    m_newImage.target.GetComponent<RectTransform>().localScale = settings.initDefine.scale;
                }

                m_newImage.Play();
                m_exist.Play();
            }
            else if (settings.type == ADV_LayerTransitionType.UseExisting)
            {
                m_exist.Release();
                //如果是使用当前图层 位移图层位置
                if (settings.image)
                {
                    m_exist.target.sprite = settings.image;
                }
                m_exist.animeDefine = ScriptableObject.CreateInstance<AnimeQueueDefine>();
                m_exist.animeDefine.animeQueue = CreatePositionAnimeQueue(m_exist.target.GetComponent<RectTransform>().localPosition, settings.initDefine.position, settings.initDuration, settings.initDelay);
                m_exist.target.GetComponent<RectTransform>().localScale = settings.initDefine.scale;

                m_exist.Play();
            }
        }

        //对话背景演出
        public void DoBackgroundAnime(ADV_Drama_Composition compistion, Action Callback = null)
        {
            if (compistion.backgroundImage == null) return;
            Image_Queue_UIAnime m_exist = GetExist();
            Image_Queue_UIAnime m_newImage = CreateAndSetFadeAnime(m_exist, compistion.backgroundImage, compistion.backgroundRuleImage, compistion.backgroundFadeDuration, 0.1f, Callback);
            m_newImage.Play();
            m_exist.Play();
        }

        public Image_Queue_UIAnime Peek()
        {
            return m_imageQueue.Peek();
        }

        Image_Queue_UIAnime GetExist()
        {
            if (m_imageQueue.Count == 0)
            {
                m_imageQueue.Enqueue(CreateImage());
            }
            Image_Queue_UIAnime m_exist;
            if (!m_imageQueue.TryPeek(out m_exist)) { return null; }
            return m_exist;
        }

        /// <summary>
        /// 渐变图层
        /// </summary>
        Image_Queue_UIAnime CreateAndSetFadeAnime(Image_Queue_UIAnime m_exist, Sprite image, Texture ruleImage, float duration = 0.8f, float delay = 0.1f, Action Callback = null)
        {
            Image_Queue_UIAnime m_newImage = CreateImage();
            m_imageQueue.Enqueue(m_newImage);
            //设置参数
            SetRule(m_newImage, ruleImage);
            SetImageSettings_Anime_Queue(m_newImage, image, CreateFadeAnimeQueue(false, duration, delay));
            //fade退出原有
            SetRule(m_exist, null);
            SetImageSettings_FadeOut_Queue(m_exist, duration, delay);

            m_exist.OnFadeComplete += () =>
            {
                m_imageQueue.Dequeue();
                Destroy(m_exist.gameObject);
                Callback?.Invoke();
            };
            return m_newImage;
        }

        /// <summary>
        /// 创建Layer内Image
        /// </summary>
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
        void SetImageSettings_FadeOut_Queue(Image_Queue_UIAnime m_ImageUIAnime, float fadeDuration = 0.8f, float delay = 0.1f)
        {
            m_ImageUIAnime.animeDefine = ScriptableObject.CreateInstance<AnimeQueueDefine>();
            m_ImageUIAnime.animeDefine.animeFadeQueue = CreateFadeAnimeQueue(true, fadeDuration, delay);
        }

        /// <summary>
        /// 设置Shader Rule Image
        /// </summary>
        void SetRule(Image_Queue_UIAnime m_ImageUIAnime, Texture ruleImage)
        {
            m_ImageUIAnime.target.material.SetFloat("_USE_RULE_TEX", ruleImage == null ? 0.0f : 1.0f);
            m_ImageUIAnime.target.material.SetTexture("_RuleTex", ruleImage);
        }

        AnimeQueueSettings[] CreateFadeAnimeQueue(bool isFadeOut = false, float duration = 0.8f, float delay = 0.1f)
        {
            AnimeQueueSettings[] m_queue = new AnimeQueueSettings[] { new AnimeQueueSettings() };
            m_queue[0].animeType = AnimeType.ShaderFloatProperty;
            m_queue[0].curve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
            m_queue[0].delay = delay;
            m_queue[0].duration = duration;
            m_queue[0].from = isFadeOut ? Vector4.one : Vector4.zero;
            m_queue[0].to = isFadeOut ? Vector4.zero : Vector4.one;
            m_queue[0].propertyName = "_Progress";
            return m_queue;
        }

        AnimeQueueSettings[] CreatePositionAnimeQueue(Vector3 from, Vector3 to, float duration = 0.8f, float delay = 0.1f)
        {
            AnimeQueueSettings[] m_queue = new AnimeQueueSettings[] { new AnimeQueueSettings() };
            m_queue[0].animeType = AnimeType.Position;
            m_queue[0].curve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
            m_queue[0].delay = delay;
            m_queue[0].duration = duration;
            m_queue[0].from = from;
            m_queue[0].to = to;
            return m_queue;
        }
    }
}