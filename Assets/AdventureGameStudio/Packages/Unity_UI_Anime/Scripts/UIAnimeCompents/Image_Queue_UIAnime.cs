using Sakuya.UnityUIAnime.Define;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sakuya.UnityUIAnime
{
    public class Image_Queue_UIAnime : CompUIAnime<Image, AnimeQueueDefine>
    {
        bool recordedPos = false;
        protected Vector3 anchoredPositionOrigin;
        bool recordedRot = false;
        protected Vector3 anchoredRotOrigin;
        bool recordedScale = false;
        protected Vector3 anchoredScaleOrigin;
        Dictionary<string, float> m_originFloatValue = new Dictionary<string, float>();

        Dictionary<string, Vector3> m_originPool = new Dictionary<string, Vector3>();

        public Action OnFadeComplete;
        public override void Play()
        {
            Dispose();
            RectTransform m_rect = target.GetComponent<RectTransform>();
            if (!recordedPos)
            {
                anchoredPositionOrigin = m_rect.anchoredPosition;
                recordedPos = true;
            }
            if (!recordedRot)
            {
                anchoredRotOrigin = m_rect.localEulerAngles;
                recordedRot = true;
            }
            if (!recordedScale)
            {
                anchoredScaleOrigin = m_rect.localScale;
                recordedScale = true;
            }
            if (animeDefine.animeQueue != null)
            {
                for (int i = 0; i < animeDefine.animeQueue.Length; i++)
                {
                    if (animeDefine.animeQueue[i].animeType == AnimeType.ShaderFloatProperty && !m_originFloatValue.ContainsKey(animeDefine.animeQueue[i].propertyName))
                    {
                        m_originFloatValue.Add(animeDefine.animeQueue[i].propertyName, target.material.GetFloat(animeDefine.animeQueue[i].propertyName));
                    }
                }
            }
            if (animeDefine.animeFadeQueue != null)
            {
                for (int i = 0; i < animeDefine.animeFadeQueue.Length; i++)
                {
                    if (animeDefine.animeFadeQueue[i].animeType == AnimeType.ShaderFloatProperty && !m_originFloatValue.ContainsKey(animeDefine.animeFadeQueue[i].propertyName))
                        m_originFloatValue.Add(animeDefine.animeFadeQueue[i].propertyName, target.material.GetFloat(animeDefine.animeFadeQueue[i].propertyName));
                }
            }
            base.Play();
        }

        public void Release()
        {
            //RectTransform m_rect = target.GetComponent<RectTransform>();
            recordedPos = false;
            recordedPos = false;
            recordedScale = false;
            m_originFloatValue = new Dictionary<string, float>();
            m_originPool = new Dictionary<string, Vector3>();
            base.Dispose();
        }

        public override void Dispose()
        {
            RectTransform m_rect = target.GetComponent<RectTransform>();
            if (recordedPos)
            {
                m_rect.anchoredPosition = anchoredPositionOrigin;
                recordedPos = false;
            }
            if (recordedRot)
            {
                m_rect.localEulerAngles = anchoredRotOrigin;
                recordedPos = false;
            }
            if (recordedScale)
            {
                m_rect.localScale = anchoredScaleOrigin;
                recordedScale = false;
            }
            foreach (var item in m_originFloatValue)
            {
                target.material.SetFloat(item.Key, item.Value);
            }
            m_originFloatValue = new Dictionary<string, float>();
            m_originPool = new Dictionary<string, Vector3>();
            base.Dispose();
        }

        protected override void PlayAnimeByTime()
        {
            SetAnimeByTime(animeDefine.animeFadeQueue, DoFadeQueue, () => { OnFadeComplete?.Invoke(); Pause(); });

            SetAnimeByTime(animeDefine.animeQueue, DoLayerAnimeQuene, () =>
            {
                if (!loop)
                {
                    OnComplete?.Invoke();
                }
                BaseLoopPlay();
            });
        }

        void DoLayerAnimeQuene(AnimeQueueSettings animeSetting, float m_time, float index)
        {
            RectTransform m_rect = target.GetComponent<RectTransform>();
            if (animeSetting.animeType == AnimeType.Position)
            {
                Vector3 m_orgin;
                if (!m_originPool.TryGetValue("Pos" + index, out m_orgin))
                {
                    m_originPool.Add("Pos" + index, m_rect.anchoredPosition);
                    m_orgin = m_rect.anchoredPosition;
                }
                Vector3 m_value = CalcVactorValueByTime(animeSetting, m_orgin, animeSetting.relative, m_time);
                m_rect.anchoredPosition = m_value;
            }
            else if (animeSetting.animeType == AnimeType.Rotation)
            {
                Vector3 m_orgin;
                if (!m_originPool.TryGetValue("Rot" + index, out m_orgin))
                {
                    m_originPool.Add("Rot" + index, m_rect.localEulerAngles);
                    m_orgin = m_rect.localEulerAngles;
                }
                Vector3 m_value = CalcVactorValueByTime(animeSetting, m_orgin, animeSetting.relative, m_time);
                target.GetComponent<RectTransform>().localEulerAngles = m_value;
            }
            else if (animeSetting.animeType == AnimeType.Scale)
            {
                Vector3 m_orgin;
                if (!m_originPool.TryGetValue("Scale" + index, out m_orgin))
                {
                    m_originPool.Add("Scale" + index, m_rect.localScale);
                    m_orgin = m_rect.localScale;
                }
                Vector3 m_value = CalcVactorValueByTime(animeSetting, m_orgin, animeSetting.relative, m_time);
                target.GetComponent<RectTransform>().localScale = m_value;
            }
            DoFadeQueue(animeSetting, m_time, index);
        }

        void DoFadeQueue(AnimeQueueSettings animeSetting, float m_time, float index)
        {
            if (animeSetting.animeType == AnimeType.ShaderFloatProperty)
            {
                target.material.SetFloat(animeSetting.propertyName, CalcFloatValueByTime(animeSetting, m_time));
            }
        }

        Vector3 CalcVactorValueByTime(AnimeQueueSettings setting, Vector3 orgin, bool relative, float time)
        {
            Vector3 m_from = relative ? orgin + (Vector3)setting.from : (Vector3)setting.from;
            Vector3 m_to = relative ? orgin + (Vector3)setting.to : (Vector3)setting.to;
            Vector3 res = Vector3.Lerp(m_from, m_to, setting.curve.Evaluate(time / setting.duration));
            return res;
        }

        float CalcFloatValueByTime(AnimeQueueSettings setting, float time)
        {
            return Mathf.Lerp(setting.from.x, setting.to.x, setting.curve.Evaluate(time / setting.duration));
        }
    }
}