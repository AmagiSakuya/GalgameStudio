using Sakuya.UnityUIAnime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace AdventureGame
{
    public class AdventureGameImageFader : MonoBehaviour
    {
        public GameObject imagePrefab;

        [HideInInspector] public ImageMaterial_UIAnime[] m_imagePool;

        Queue<Image_Queue_UIAnime> m_imageQueue;

        private void Awake()
        {
            Init();
        }

        void Init()
        {
            m_imageQueue = new Queue<Image_Queue_UIAnime>();
        }

        Image_Queue_UIAnime CreateImage()
        {
            GameObject m_image = Instantiate(imagePrefab, gameObject.transform);
            m_image.GetComponent<Image>().material = new Material(Shader.Find("Hidden/ADVGame/Image"));
            return m_image.GetComponent<Image_Queue_UIAnime>();
        }

        //public void DoBackgroundAnime(ADV_Drama_Composition compistion)
        //{
        //    //判断队列数量
        //    if (m_imageQueue.Count == 0)
        //    {
        //        m_imageQueue.Enqueue(CreateImage());
        //    }
        //    //判断类型
        //    if (backgroundSettings.type == ADV_LayerTransitionType.UseExisting)
        //    {
        //        Image_Queue_UIAnime m_exist;
        //        if (!m_imageQueue.TryPeek(out m_exist)) { return; }
        //        //设置参数
        //        SetRule(m_exist, backgroundSettings);
        //        SetImage_Queue_UIAnimeSettings(m_exist, backgroundSettings);
        //        //执行动画
        //        m_exist.Play();
        //    }
        //    else if (backgroundSettings.type == ADV_LayerTransitionType.ReplaceExisting)
        //    {
        //        Image_Queue_UIAnime m_exist;
        //        if (!m_imageQueue.TryPeek(out m_exist)) { return; }
        //        Image_Queue_UIAnime m_newImage = CreateImage();
        //        m_imageQueue.Enqueue(m_newImage);
        //        //设置参数
        //        SetRule(m_newImage, backgroundSettings);
        //        SetImage_Queue_UIAnimeSettings(m_newImage, backgroundSettings);
        //        m_newImage.Play();
        //        //fade退出原有
        //        SetRule(m_exist, null);
        //        SetImage_Queue_UIAnimeSettings(m_exist, null);
        //        m_exist.OnComplete += () =>
        //        {
        //            m_imageQueue.Dequeue();
        //            Destroy(m_exist.gameObject);
        //        };
        //        m_exist.Play();
        //    }
        //}

        public void DoBackgroundAnime(ADV_Drama_Composition compistion)
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
            var settings = new ADV_BackgroundImageTransition();
            settings.image = compistion.backgroundImage;
            settings.ruleImage = compistion.backgroundRuleImage;
            settings.anime = CreateFadeAnimeQueue(false, compistion.backgroundFadeDuration);
            SetRule(m_newImage, settings);
            SetImage_Queue_UIAnimeSettings(m_newImage, settings);
            //fade退出原有
            SetRule(m_exist, null);
            SetImage_Queue_UIAnimeSettings(m_exist, null, compistion.backgroundFadeDuration);

            m_exist.OnComplete += () =>
            {
                m_imageQueue.Dequeue();
                Destroy(m_exist.gameObject);
            };

            m_newImage.Play();
            m_exist.Play();

        }

        void SetImage_Queue_UIAnimeSettings(Image_Queue_UIAnime m_ImageUIAnime, ADV_BackgroundImageTransition backgroundSettings, float fadeDuration = 0.8f)
        {
            if (backgroundSettings != null)
            {
                m_ImageUIAnime.target.sprite = backgroundSettings.image;
                m_ImageUIAnime.animeDefine = new Sakuya.UnityUIAnime.Define.AnimeQueueDefine();
                m_ImageUIAnime.animeDefine.animeQueue = backgroundSettings.anime;
            }
            else
            {
                m_ImageUIAnime.animeDefine = new Sakuya.UnityUIAnime.Define.AnimeQueueDefine();
                m_ImageUIAnime.animeDefine.animeQueue = CreateFadeAnimeQueue(true, fadeDuration);
            }
        }

        private static void SetRule(Image_Queue_UIAnime m_ImageUIAnime, ADV_BackgroundImageTransition backgroundSettings)
        {
            if (backgroundSettings == null || backgroundSettings.ruleImage == null)
            {
                m_ImageUIAnime.target.material.SetFloat("_USE_RULE_TEX", 0.0f);
                m_ImageUIAnime.target.material.SetTexture("_RuleTex", null);
            }

            if (backgroundSettings != null && backgroundSettings.ruleImage)
            {
                m_ImageUIAnime.target.material.SetFloat("_USE_RULE_TEX", 1.0f);
                m_ImageUIAnime.target.material.SetTexture("_RuleTex", backgroundSettings.ruleImage);
            }
        }

        AnimeQueueSettings[] CreateFadeAnimeQueue(bool isFadeOut = false, float duration = 0.8f)
        {
            AnimeQueueSettings[] m_queue = new AnimeQueueSettings[] { new AnimeQueueSettings() };
            m_queue[0].animeType = AnimeType.ShaderFloatProperty;
            m_queue[0].curve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
            m_queue[0].delay = 0.1f;
            m_queue[0].duration = duration;
            if (isFadeOut)
            {
                m_queue[0].from = Vector4.one;
                m_queue[0].to = Vector4.zero;
            }
            else
            {
                m_queue[0].from = Vector4.zero;
                m_queue[0].to = Vector4.one;
            }
            m_queue[0].propertyName = "_Progress";
            return m_queue;
        }
    }
}