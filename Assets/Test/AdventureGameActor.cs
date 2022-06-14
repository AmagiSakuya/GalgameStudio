using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AdventureGame
{
    [RequireComponent(typeof(Image))]
    public class AdventureGameActor : MonoBehaviour
    {
        string m_ADVImageShaderName = "Hidden/ADVGame/Image";
        bool lockProgress;

        Image m_target;
        public Image target
        {
            get
            {
                if (m_target == null) m_target = GetComponent<Image>();
                if (!m_target.material.shader.name.Equals(m_ADVImageShaderName)) m_target.material = new Material(Shader.Find(m_ADVImageShaderName));
                return m_target;
            }
        }

        RectTransform m_rect;
        public RectTransform rect
        {
            get
            {
                if (m_rect == null) m_rect = GetComponent<RectTransform>();
                return m_rect;
            }
        }

        #region Fade�л�

        /// <summary>
        /// ����Shader Rule Image
        /// </summary>
        public void SetFadeProgress(float progress)
        {
            if (lockProgress) return;
            target.material.SetFloat("_Progress", progress);
        }

        /// <summary>
        /// ����Shader Rule Image
        /// </summary>
        public void SetRule(Texture ruleImage)
        {
            target.material.SetFloat("_USE_RULE_TEX", ruleImage == null ? 0.0f : 1.0f);
            target.material.SetTexture("_RuleTex", ruleImage);
        }

        /// <summary>
        /// ���RuleImage
        /// </summary>
        public void ClearRule()
        {
            SetRule(null);
            SetProgressLock(false);
        }

        /// <summary>
        /// ����Progress
        /// </summary>
        public void SetProgressLock(bool locked)
        {
            lockProgress = locked;
        }

        /// <summary>
        /// ��ȡ�Ƿ�����Progress
        /// </summary>
        public bool GetProgressLock()
        {
            return lockProgress;
        }

        #endregion

        #region ��תλ�������ݳ�
        public void SetTransform(AdventureGameActorPlayableBehavior settings, float duration, float time)
        {
            rect.anchoredPosition = CalcVactorValueByTime(settings.posAnime, duration, time);
            rect.localEulerAngles = CalcVactorValueByTime(settings.rotationAnime, duration, time);
            rect.localScale = CalcVactorValueByTime(settings.scaleAnime, duration, time);
        }
        Vector2 CalcVactorValueByTime(ActorTrackAnimeSettings settings, float duration, float time)
        {
            Vector3 res = Vector3.Lerp(settings.from, settings.to, settings.curve.Evaluate(time / duration));
            return res;
        }
        #endregion
    }
}