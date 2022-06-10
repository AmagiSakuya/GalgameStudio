using Sakuya.UnityUIAnime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

// A behaviour that is attached to a playable
[System.Serializable]
public class AdventureGameAnimePlayableBehavior : PlayableBehaviour
{
    public Sprite backgroundImage;
    public Texture backgroundRuleImage;
    public float backgroundFadeDuration = 0.8f;

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
        Debug.Log("Start");
        //playable.get
        //Image_Queue_UIAnime a = (Image_Queue_UIAnime)playable;
    }

    // Called when the state of the playable is set to Paused
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        Debug.Log("Pause");
    }

    // Called each frame while the state is set to Play
    public override void PrepareFrame(Playable playable, FrameData info)
    {
        //Debug.Log("Prepare");
    }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        Image_Queue_UIAnime a = (Image_Queue_UIAnime)playerData;
        int inputCount = playable.GetInputCount();
        for(int i = 0; i < inputCount; i++)
        {
            var inputWeight = playable.GetInputWeight(i);
        }
        //Debug.Log(playable.GetTime());
        //base.ProcessFrame(playable, info, playerData);
    }
}
