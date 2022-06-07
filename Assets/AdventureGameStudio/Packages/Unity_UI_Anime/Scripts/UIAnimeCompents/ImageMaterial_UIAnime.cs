using Sakuya.UnityUIAnime.Define;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sakuya.UnityUIAnime
{
    public class ImageMaterial_UIAnime : CompUIAnime<Image, Shader_UIAnime_Define>
    {

        Dictionary<string, float> m_originFloatValue = new Dictionary<string, float>();

        public override void Play()
        {
            Dispose();
            SaveFloat();
            base.Play();
        }

        public override void Dispose()
        {
            DisposeFloat();
            base.Dispose();
        }

        protected override void PlayAnimeByTime()
        {
            SetAnimeByTime(animeDefine.floatAnimes, (UIAnimeShaderFloatSettings animeSetting, float m_time) =>
            {
                target.material.SetFloat(animeSetting.propertyName, CalcFloatValueByTime(animeSetting, m_time));
            }, BaseLoopPlay);
        }

        float CalcFloatValueByTime(UIAnimeShaderFloatSettings setting, float time)
        {
            return Mathf.Lerp(setting.from, setting.to, setting.curve.Evaluate(time / setting.duration));
        }

        protected virtual void SaveFloat()
        {
            for (int i = 0; i < animeDefine.floatAnimes.Length; i++)
            {
                m_originFloatValue.Add(animeDefine.floatAnimes[i].propertyName, target.material.GetFloat(animeDefine.floatAnimes[i].propertyName));
            }
        }

        protected virtual void DisposeFloat()
        {
            foreach (var item in m_originFloatValue)
            {
                target.material.SetFloat(item.Key, item.Value);
            }
            m_originFloatValue = new Dictionary<string, float>();
        }
    }
}

