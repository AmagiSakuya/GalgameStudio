﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sakuya.UnityUIAnime
{
    public abstract class UIAnimeBase : MonoBehaviour
    {
        [ReadOnly] public bool pause;

        [ReadOnly] public float time = 0;

        public Action OnComplete;

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
            SetAnimeByTime(quene, (TSettings a, float b, float c) =>
            {
                PlayAnimeByTime(a, b);
            }, CompleteCallback);
        }

        protected void SetAnimeByTime<TSettings>(TSettings[] quene, Action<TSettings, float, float> PlayAnimeByTime, Action CompleteCallback = null) where TSettings : BaseAnimeSettings
        {
            bool isTimeInQuene = false;
            float countTime = 0;
            for (int i = 0; i < quene.Length; i++)
            {
                float m_thisQueneCostTime = quene[i].delay + quene[i].duration;
                if (countTime <= time && time <= countTime + m_thisQueneCostTime)
                {
                    float timeInthis = time - countTime;
                    float m_time = timeInthis - quene[i].delay;
                    if (quene[i].timeDecimalPoint >= 0)
                    {
                        m_time = (float)Math.Round(m_time, quene[i].timeDecimalPoint);
                    }
                    PlayAnimeByTime(quene[i], m_time, i);
                    isTimeInQuene = true;
                    break;
                }
                else
                {
                    countTime += m_thisQueneCostTime;
                }
            }

            if (isTimeInQuene) return;
            CompleteCallback?.Invoke();
        }
    }
}