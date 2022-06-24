using Sakuya.UnityUIAnime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace AdventureGame
{

    [System.Serializable]
    public class ActorTrackAnimeSettings
    {
        public Vector2 from;
        public Vector2 to;
        public AnimationCurve curve; //AnimationCurve.Linear(0, 0, 1, 1);

        public ActorTrackAnimeSettings() { }
        public ActorTrackAnimeSettings(Vector2 m_default)
        {
            from = m_default;
            to = m_default;
        }
    }

    // A behaviour that is attached to a playable
    [System.Serializable]
    public class AdventureGameActorPlayableBehavior : PlayableBehaviour
    {
        public AdventureGameLayer binding { get; set; }

        public Sprite image;
        public AdventureGameFgImageDefine fgImageDefine;
        public Texture ruleImage;
        public bool setNativeSize;
        public AdventureGameActorTransformPresets transformPresets;
        public ActorTrackAnimeSettings posAnime;
        public ActorTrackAnimeSettings rotationAnime;
        public ActorTrackAnimeSettings scaleAnime = new ActorTrackAnimeSettings(Vector2.one);
        public bool lockFade = false;
        [ReadOnly] public string bodyName;
        [ReadOnly] public string faceName;
        [ReadOnly] public string append1;
        [ReadOnly] public string append2;
        [ReadOnly] public string append3;

        // Called when the owning graph starts playing
        public override void OnGraphStart(Playable playable)
        {
            //Debug.Log("OnGraphStart");
        }

        // Called when the owning graph stops playing
        public override void OnGraphStop(Playable playable)
        {
            //Debug.Log("OnGraphStop");
            binding.Clear();
        }

        // Called when the state of the playable is set to Play
        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            binding.OnBehaviorStart(this);
        }

        // Called when the state of the playable is set to Paused
        public override void OnBehaviourPause(Playable playable, FrameData info)
        {
            if (playable.GetPlayState() != PlayState.Playing)
            {
                binding.OnBehaviorEnd(this);
            }
        }

        // Called each frame while the state is set to Play
        public override void PrepareFrame(Playable playable, FrameData info)
        {
            //Debug.Log("Prepare");
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            binding.OnProcessFrame(this, playable, info);
        }
    }
}