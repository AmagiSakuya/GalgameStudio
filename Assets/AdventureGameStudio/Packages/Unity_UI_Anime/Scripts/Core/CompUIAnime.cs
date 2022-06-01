using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sakuya.UnityUIAnime
{
    public abstract class CompUIAnime<TComp, TSettings> : UIAnimeBase where TComp : Component where TSettings : ScriptableObject
    {
        [SerializeField] TComp m_target;
        [SerializeField] protected bool m_playOnEnable;
        [SerializeField] protected bool loop;
        [SerializeField] TSettings m_animeDefine;

        Coroutine m_animeCoroutine;

        protected TComp target
        {
            get
            {
                if (m_target == null)
                {
                    m_target = GetComponent<TComp>();
                    if (m_target == null) { m_target = gameObject.AddComponent<TComp>(); }
                }
                return m_target;
            }
            set
            {
                m_target = value;
            }
        }

        protected TSettings animeDefine
        {
            get
            {
                return m_animeDefine;
            }
            set
            {
                m_animeDefine = value;
            }
        }

        private void Awake()
        {
            Dispose();
        }

        private void OnEnable()
        {
            if (!m_playOnEnable) return;
            Play();
        }

        private void OnDisable()
        {
            Dispose();
        }

        public override void Play()
        {
            m_animeCoroutine = StartCoroutine(AnimeUpdater());
        }

        public override void Pause()
        {
            pause = true;
        }

        public override void Resume()
        {
            pause = false;
        }

        public override void Dispose()
        {
            if (m_animeCoroutine != null)
            {
                StopCoroutine(m_animeCoroutine);
            }
            m_animeCoroutine = null;
            time = 0;
            Resume();
        }

        protected void BaseLoopPlay()
        {
            if (loop)
            {
                time = 0;
            }
            else
            {
                Pause();
            }
        }

        IEnumerator AnimeUpdater()
        {
            while (true)
            {
                while (!pause)
                {
                    time += Time.deltaTime;
                    PlayAnimeByTime();
                    yield return null;

                }
                while (pause)
                {
                    yield return null;
                }
            }
        }
    }
}