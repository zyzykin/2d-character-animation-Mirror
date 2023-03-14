using UnityEngine;

[CreateAssetMenu(fileName = "New Animation Library", menuName = "Animation Library")]
public class AnimationLibrary : ScriptableObject
{
    public AnimationSequence[] sequences;
}