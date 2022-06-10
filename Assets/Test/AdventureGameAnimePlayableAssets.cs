using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class AdventureGameAnimePlayableAssets : PlayableAsset
{
    public AdventureGameAnimePlayableBehavior settings = new AdventureGameAnimePlayableBehavior();
    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        return ScriptPlayable<AdventureGameAnimePlayableBehavior>.Create(graph, settings);
    }
}
