using Sakuya.UnityUIAnime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace AdventureGame
{
    [TrackBindingType(typeof(AdventureGameLayer))]
    [TrackClipType(typeof(AdventureGameActorPlayableAssets))]
    public class AdventureGameActorTrackAssets : TrackAsset
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            var director = go.GetComponent<PlayableDirector>();
            var binding = director.GetGenericBinding(this);

            foreach (var c in GetClips())
            {
                var myAsset = c.asset as AdventureGameActorPlayableAssets;
                if (myAsset != null)
                    myAsset.binding = (AdventureGameLayer)binding;
            }

            return ScriptPlayable<AdventureGameActorTrackMixBehavior>.Create(graph, inputCount);
        }
    }
}