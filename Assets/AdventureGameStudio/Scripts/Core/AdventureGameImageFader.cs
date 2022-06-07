using Sakuya.UnityUIAnime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AdventureGame
{
    public class AdventureGameImageFader : MonoBehaviour
    {
        public GameObject imagePrefab;

        [HideInInspector] public ImageMaterial_UIAnime[] m_imagePool;

        private void Awake()
        {
            Init();
        }

        void Init()
        {
            m_imagePool = new ImageMaterial_UIAnime[2];
            m_imagePool[0] = CreateImage();
            m_imagePool[1] = CreateImage();
        }

        ImageMaterial_UIAnime CreateImage()
        {
            GameObject m_image = GameObject.Instantiate(imagePrefab, gameObject.transform);
            m_image.GetComponent<Image>().material = new Material(Shader.Find("Hidden/ADVGame/Image"));
            return m_image.GetComponent<ImageMaterial_UIAnime>();
        }

        public void PrepareImage(Sprite image, float duration = -1.0f, Texture ruleImage = null, bool setNativeSize = false)
        {

            //�ͷ�
            m_imagePool[0].Dispose();
            //������ͼ�浽ǰ����
            m_imagePool[1].target.sprite = m_imagePool[0].target.sprite;
            //����ͼ�浽����
            m_imagePool[0].target.sprite = image;
            if (setNativeSize)
            {
                m_imagePool[0].target.SetNativeSize();
                m_imagePool[1].target.SetNativeSize();
            }
            //���ò���
            if (ruleImage)
            {
                m_imagePool[0].target.material.SetFloat("_USE_RULE_TEX", 1.0f);
                m_imagePool[0].target.material.SetTexture("_RuleTex", ruleImage);
            }
            else
            {
                m_imagePool[0].target.material.SetFloat("_USE_RULE_TEX", 0.0f);
                m_imagePool[0].target.material.SetTexture("_RuleTex", null);
            }
            if (duration < 0)
            {
                duration = 0.8f;
            }
            m_imagePool[0].animeDefine.floatAnimes[0].duration = duration;

        }

        public void DoImageFadeAnime(Sprite image, float duration = -1.0f, Texture ruleImage = null)
        {
            PrepareImage(image, duration, ruleImage);
            //����
            m_imagePool[1].Play();
        }

    }
}