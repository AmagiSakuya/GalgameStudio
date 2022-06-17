using Sakuya.UnityUIAnime;
using Sakuya.UnityUIAnime.Define;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

namespace AdventureGame
{
    public class AdventureGameLayer : MonoBehaviour
    {
        public GameObject actorPrefab;

        RectTransform_UIAnime m_rectUIAnime;
        public RectTransform_UIAnime rectUIAnime
        {
            get
            {
                if (m_rectUIAnime == null) m_rectUIAnime = gameObject.GetComponent<RectTransform_UIAnime>();
                if (m_rectUIAnime == null) m_rectUIAnime = gameObject.AddComponent<RectTransform_UIAnime>();
                return m_rectUIAnime;
            }
        }

        Dictionary<AdventureGameActorPlayableBehavior, GameObject> m_pool = new Dictionary<AdventureGameActorPlayableBehavior, GameObject>();

        #region TimeLine���Ŵ���
        public void OnBehaviorStart(AdventureGameActorPlayableBehavior behavior)
        {
            // ����Actor
            if (ContainsKey(behavior)) return;
            SetProgressLock(behavior.ruleImage != null);
            m_pool[behavior] = Instantiate(actorPrefab, gameObject.transform);
            // ����image
            var actor = m_pool[behavior].GetComponent<AdventureGameActor>();
            if (behavior.image)
            {
                actor.target.sprite = behavior.image;
            }
            if (behavior.setNativeSize)
            {
                actor.target.SetNativeSize();
            }
            actor.SetRule(behavior.ruleImage);
        }

        public void OnBehaviorEnd(AdventureGameActorPlayableBehavior behavior)
        {
            //ɾ��Actor
            if (!ContainsKey(behavior)) return;
            ClearBehaviorActor(behavior);
            //�������ruleImage����
            ClearCilpsRuleImage();
        }

        public void OnProcessFrame(AdventureGameActorPlayableBehavior behavior, Playable playable, FrameData info)
        {
            //Debug.Log(playable.GetTime());
        }

        void ClearBehaviorActor(AdventureGameActorPlayableBehavior behavior)
        {
            if (Application.isPlaying) Destroy(m_pool[behavior]);
            else DestroyImmediate(m_pool[behavior]);
            m_pool.Remove(behavior);
        }
        #endregion

        #region ��������
        /// <summary>
        /// ���Actors
        /// </summary>
        public void Clear()
        {
            foreach (var item in m_pool)
            {
                if (Application.isPlaying) Destroy(item.Value);
                else DestroyImmediate(item.Value);
            }

            m_pool = new Dictionary<AdventureGameActorPlayableBehavior, GameObject>();
        }

        public AdventureGameActor GetActorByBehavior(AdventureGameActorPlayableBehavior behavior)
        {
            GameObject result;
            if (m_pool.TryGetValue(behavior, out result))
            {
                return result.GetComponent<AdventureGameActor>();
            }
            return null;
        }
        #endregion

        #region ˽�з���
        bool ContainsKey(AdventureGameActorPlayableBehavior behavior)
        {
            return m_pool.ContainsKey(behavior);
        }

        void ClearCilpsRuleImage()
        {
            foreach (var item in m_pool)
            {
                item.Value.GetComponent<AdventureGameActor>().ClearRule();
            }
        }

        void SetProgressLock(bool locked)
        {
            foreach (var item in m_pool)
            {
                item.Value.GetComponent<AdventureGameActor>().SetProgressLock(locked);
            }
        }
        #endregion

        #region ͼ�㶯��
        public void PlayLayerLoopAnime(RectTransform_UIAnime_Define define)
        {
            if (define != null)
            {
                rectUIAnime.playOnEnable = false;
                rectUIAnime.loop = true;
                rectUIAnime.animeDefine = define;
                rectUIAnime.Play();
            }
            else
            {
                rectUIAnime.Dispose();
            }
        }
        #endregion

    }
}