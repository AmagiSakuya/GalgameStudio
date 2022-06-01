using Sakuya.UnityUIAnime.Define;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sakuya.UnityUIAnime
{
    public class Text_UIAnime : CompUIAnime<Text, Text_UIAnime_Define>
    {
        protected string m_textOrigin;

        public override void Play()
        {
            Dispose();
            if (string.IsNullOrEmpty(m_textOrigin))
            {
                m_textOrigin = target.text;
            }
            base.Play();
        }

        public override void Dispose()
        {
            DisposeTypeWritter();
            base.Dispose();
        }

        protected void DisposeTypeWritter()
        {
            if (!string.IsNullOrEmpty(m_textOrigin))
            {
                target.text = m_textOrigin;
                m_textOrigin = string.Empty;
            }
        }

        protected override void PlayAnimeByTime()
        {
            SetAnimeByTime(new BaseAnimeSettings[] { animeDefine.typeWriterAnimeSettings }, (BaseAnimeSettings animeSetting, float m_time) =>
             {
                 target.text = GetTypeWrittenAnimeValueByTime(m_time);
             }, BaseLoopPlay);
        }

        string GetTypeWrittenAnimeValueByTime(float time)
        {
            int splitIndex = (int)Mathf.Ceil(Mathf.Lerp(0, m_textOrigin.Length, animeDefine.typeWriterAnimeSettings.curve.Evaluate(time / animeDefine.typeWriterAnimeSettings.duration)));
            return m_textOrigin.Substring(0, splitIndex);
        }
    }
}