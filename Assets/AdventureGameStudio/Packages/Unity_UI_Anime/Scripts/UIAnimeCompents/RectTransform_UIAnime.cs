using Sakuya.UnityUIAnime.Define;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sakuya.UnityUIAnime
{
    public class RectTransform_UIAnime : CompUIAnime<RectTransform, RectTransform_UIAnime_Define>
    {
        bool recordedPos = false;
        protected Vector3 anchoredPositionOrigin;
        bool recordedRot = false;
        protected Vector3 anchoredRotOrigin;
        bool recordedScale = false;
        protected Vector3 anchoredScaleOrigin;

        bool moveComplete;
        bool rotComplete;
        bool scaleComplete;

        #region 动画
        public override void Play()
        {
            Dispose();
            if (!recordedPos)
            {
                anchoredPositionOrigin = target.anchoredPosition;
                recordedPos = true;
            }
            if (!recordedRot)
            {
                anchoredRotOrigin = target.localEulerAngles;
                recordedRot = true;
            }
            if (!recordedScale)
            {
                anchoredScaleOrigin = target.localScale;
                recordedScale = true;
            }
            base.Play();
        }

        public override void Dispose()
        {
            DisposeMove();
            DisposeRotate();
            DisposeScale();
            base.Dispose();
        }

        protected override void PlayAnimeByTime()
        {
            SetAnimeByTime(animeDefine.positionAnimeQueue, (Vector3AnimeSettings animeSetting, float m_time) =>
            {
                target.anchoredPosition = CalcVactorValueByTime(animeSetting, anchoredPositionOrigin, animeDefine.relativePos, m_time);
            }, () =>
            {
                moveComplete = true;
            });

            SetAnimeByTime(animeDefine.rotationAnimeQueue, (Vector3AnimeSettings animeSetting, float m_time) =>
            {
                target.localEulerAngles = CalcVactorValueByTime(animeSetting, anchoredRotOrigin, animeDefine.relativeRotation, m_time);
            }, () =>
            {
                rotComplete = true;
            });

            SetAnimeByTime(animeDefine.scaleAnimeQueue, (Vector3AnimeSettings animeSetting, float m_time) =>
            {
                target.localScale = CalcVactorValueByTime(animeSetting, anchoredScaleOrigin, animeDefine.relativeScale, m_time);
            }, () =>
            {
                scaleComplete = true;
            });

            if (moveComplete && rotComplete && scaleComplete)
            {
                BaseLoopPlay();
                moveComplete = false;
                rotComplete = false;
                scaleComplete = false;
            }
        }
        #endregion


        protected virtual void DisposeMove()
        {
            if (recordedPos)
            {
                target.anchoredPosition = anchoredPositionOrigin;
                recordedPos = false;
            }
            moveComplete = false;
        }

        protected virtual void DisposeRotate()
        {
            if (recordedRot)
            {
                target.localEulerAngles = anchoredRotOrigin;
                recordedPos = false;
            }
            rotComplete = false;
        }

        protected virtual void DisposeScale()
        {
            if (recordedScale)
            {
                target.localScale = anchoredScaleOrigin;
                recordedScale = false;
            }
            scaleComplete = false;
        }

        Vector3 CalcVactorValueByTime(Vector3AnimeSettings setting, Vector3 orgin, bool relative, float time)
        {
            Vector3 m_from = relative ? orgin + setting.from : setting.from;
            Vector3 m_to = relative ? orgin + setting.to : setting.to;
            Vector3 res = Vector3.Lerp(m_from, m_to, setting.curve.Evaluate(time / setting.duration));
            return res;
        }

    }
}