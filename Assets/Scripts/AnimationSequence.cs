using System;
using UnityEngine;

[Serializable]
public class AnimationSequence
{
    public string name;
    public Sprite[] frames;
    public float frameRate = 12f;
    public bool loop = true;
}