using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace AdventureGame
{
    [System.Serializable]
    public class AdventureGameActorPlayableAssets : PlayableAsset
    {
        public AdventureGameLayer binding { get; set; }

        public AdventureGameActorPlayableBehavior settings = new AdventureGameActorPlayableBehavior();

        public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
        {
            //settings.scaleAnime.from = Vector2.one;
            //settings.scaleAnime.to = Vector2.one;
            var handle = ScriptPlayable<AdventureGameActorPlayableBehavior>.Create(graph, settings);
            handle.GetBehaviour().binding = binding;
            return handle;
        }
    }
}