using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GalgameCharacterEmoji  {
    public string EmojiName;
    public Sprite BaseImg;
    public Sprite FaceImg;
    public Vector3 Position;
    public Vector3 Rotation;
    public Vector3 Scale;
    public bool isEditMode = false;
}