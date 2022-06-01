using Sakuya.UnityUIAnime.Define;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sakuya.UnityUIAnime
{
    public class CanvasGroupAlpha_UIAnime : CompUIAnime<CanvasGroup, CanvasGroupAlpha_UIAnime_Define>
    {
        protected float m_orignAlpha = -1;

        public override void Play()
        {
            Dispose();
            if (m_orignAlpha == -1)
            {
                m_orignAlpha = target.alpha;
            }
            base.Play();
        }

        public override void Dispose()
        {
            DisposeFade();
            base.Dispose();
        }

        protected override void PlayAnimeByTime()
        {
            SetAnimeByTime(animeDefine.alphaAnimeQueue, (FloatAnimeSettings animeSetting, float m_time) =>
             {
                 target.alpha = CalcFloatValueByTime(animeSetting, m_time);
             }, BaseLoopPlay);
        }

        protected virtual void DisposeFade()
        {
            if (m_orignAlpha != -1)
            {
                target.alpha = m_orignAlpha;
                m_orignAlpha = -1;
            }
        }

        float CalcFloatValueByTime(FloatAnimeSettings setting, float time)
        {
            return Mathf.Lerp(setting.from, setting.to, setting.curve.Evaluate(time / setting.duration));
        }
    }
}
