using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sakuya.UnityUIAnime
{
    public abstract class UIAnimeBase : MonoBehaviour
    {
        [ReadOnly] public bool pause;

        [ReadOnly] public float time = 0;

        /// <summary>
        /// 从头播放
        /// </summary>
        public abstract void Play();

        /// <summary>
        /// 暂停播放
        /// </summary>
        public abstract void Pause();

        /// <summary>
        /// 恢复播放
        /// </summary>
        public abstract void Resume();

        protected abstract void PlayAnimeByTime();

        /// <summary>
        /// 释放动画资源 还原到初始状态
        /// </summary>
        public abstract void Dispose();

        protected void SetAnimeByTime<TSettings>(TSettings[] quene, Action<TSettings, float> PlayAnimeByTime, Action CompleteCallback = null) where TSettings : BaseAnimeSettings
        {
            bool isTimeInQuene = false;
            for (int i = 0; i < quene.Length; i++)
            {
                float m_thisQueneCostTime = quene[i].delay + quene[i].duration;
                if (time <= m_thisQueneCostTime)
                {
                    float m_time = time - quene[i].delay;
                    if (quene[i].timeDecimalPoint >= 0)
                    {
                        m_time = (float)Math.Round(m_time, quene[i].timeDecimalPoint);
                    }
                    PlayAnimeByTime(quene[i], m_time);
                    isTimeInQuene = true;
                    break;
                }
                else
                {
                    PlayAnimeByTime(quene[i], m_thisQueneCostTime);
                }
            }

            if (isTimeInQuene) return;
            CompleteCallback?.Invoke();
        }
    }
}