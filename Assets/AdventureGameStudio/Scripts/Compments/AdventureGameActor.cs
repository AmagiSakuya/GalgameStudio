using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AdventureGame
{
    //[RequireComponent(typeof(Image))]
    public class AdventureGameActor : MonoBehaviour
    {

        bool lockProgress;
        Image m_target;
        public Image target
        {
            get
            {
                if (m_target == null) m_target = gameObject.GetComponent<Image>();
                if (m_target == null) m_target = gameObject.AddComponent<Image>();
                if (!m_target.material.shader.name.Equals(GetShaderName())) m_target.material = new Material(Shader.Find(GetShaderName()));
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

        protected virtual string GetShaderName()
        {
            return "Hidden/ADVGame/Image";
        }

        #region TimeLine播放处理
        public virtual void OnBehaviorStart(AdventureGameActorPlayableBehavior behavior)
        {
            // 设置image
            if (behavior.image)
            {
                target.sprite = behavior.image;
            }
            if (behavior.setNativeSize)
            {
                target.SetNativeSize();
            }
            SetRule(behavior.ruleImage);
        }
        #endregion

        #region Fade切换

        /// <summary>
        /// 设置Shader Progress
        /// </summary>
        public void SetFadeProgress(float progress)
        {
            if (lockProgress) return;
            target.material.SetFloat("_Progress", progress);
        }

        /// <summary>
        /// 设置Shader Rule Image
        /// </summary>
        public virtual void SetRule(Texture ruleImage)
        {
            target.material.SetFloat("_USE_RULE_TEX", ruleImage == null ? 0.0f : 1.0f);
            target.material.SetTexture("_RuleTex", ruleImage);
        }

        /// <summary>
        /// 清空RuleImage
        /// </summary>
        public virtual void ClearRule()
        {
            SetRule(null);
            SetProgressLock(false);
        }

        /// <summary>
        /// 锁定Progress
        /// </summary>
        public void SetProgressLock(bool locked)
        {
            lockProgress = locked;
        }

        /// <summary>
        /// 获取是否锁定Progress
        /// </summary>
        public bool GetProgressLock()
        {
            return lockProgress;
        }

        #endregion

        #region 旋转位移缩放演出
        public void SetTransform(AdventureGameActorPlayableBehavior settings, float duration, float time)
        {
            rect.anchoredPosition = CalcVactorValueByTime(settings.posAnime, duration, time, settings.transformPresets != null ? settings.transformPresets.position : Vector2.zero);
            rect.localEulerAngles = CalcVactorValueByTime(settings.rotationAnime, duration, time, settings.transformPresets != null ? settings.transformPresets.rotation : Vector2.zero);
            rect.localScale = CalcVactorValueByTime(settings.scaleAnime, duration, time, settings.transformPresets != null ? new Vector2(settings.transformPresets.scale, settings.transformPresets.scale) : Vector2.zero);
        }
        protected Vector2 CalcVactorValueByTime(ActorTrackAnimeSettings settings, float duration, float time, Vector2 offset)
        {
            Vector2 res = Vector2.Lerp(settings.from + offset, settings.to + offset, settings.curve.Evaluate(time / duration));
            return res;
        }
        #endregion


    }
}