using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GalgameAction
{
    public CharacterSelect Character;
    public string Serifu;
    public AudioClip Voice;
    public AudioClip ChangeBgm;
    public Sprite ChangeBg;
    public List<GalgameKeyframe> Keyframe;

    [System.Serializable]
    public enum CharacterSelect
    {
        曉,
        七海,
        羽月,
    }
}
