using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

namespace AdventureGame
{
    public class AdventureGameLayer : MonoBehaviour
    {
        public GameObject actorPrefab;

        Dictionary<AdventureGameActorPlayableBehavior, GameObject> m_pool = new Dictionary<AdventureGameActorPlayableBehavior, GameObject>();

        public void OnBehaviorStart(AdventureGameActorPlayableBehavior behavior)
        {
            // 创建Actor
            if (ContainsKey(behavior)) return;
            SetProgressLock(behavior.ruleImage != null);
            m_pool[behavior] = Instantiate(actorPrefab, gameObject.transform);
            // 设置image
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
            //删除Actor
            if (!ContainsKey(behavior)) return;
            ClearBehaviorActor(behavior);
            //清空所有ruleImage设置
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

        /// <summary>
        /// 清空Actors
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
    }
}