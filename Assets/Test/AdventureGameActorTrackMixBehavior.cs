using Sakuya.UnityUIAnime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace AdventureGame
{

    // A behaviour that is attached to a playable
    [System.Serializable]
    public class AdventureGameActorTrackMixBehavior : PlayableBehaviour
    {
        // Called when the owning graph starts playing
        public override void OnGraphStart(Playable playable)
        {
        }

        // Called when the owning graph stops playing
        public override void OnGraphStop(Playable playable)
        {

        }

        // Called when the state of the playable is set to Play
        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {

        }

        // Called when the state of the playable is set to Paused
        public override void OnBehaviourPause(Playable playable, FrameData info)
        {

        }

        // Called each frame while the state is set to Play
        public override void PrepareFrame(Playable playable, FrameData info)
        {

        }

        int clipCount;
        float clipWeight;
        ScriptPlayable<AdventureGameActorPlayableBehavior> cilpPlayable;
        AdventureGameActorPlayableBehavior clipBehaviour;
        AdventureGameActor actor;
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            clipCount = playable.GetInputCount();
            for (int i = 0; i < clipCount; i++)
            {
                clipWeight = playable.GetInputWeight(i);

                cilpPlayable = (ScriptPlayable<AdventureGameActorPlayableBehavior>)playable.GetInput(i);
                clipBehaviour = cilpPlayable.GetBehaviour();
                actor = clipBehaviour.binding.GetActorByBehavior(clipBehaviour);
                if (actor != null)
                {
                    actor.SetFadeProgress(clipWeight);
                    //¶¯»­
                    actor.SetTransform(clipBehaviour, (float)cilpPlayable.GetDuration(), (float)cilpPlayable.GetTime());
                }
            }
        }
    }
}