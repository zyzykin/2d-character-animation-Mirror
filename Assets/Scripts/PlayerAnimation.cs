using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] AnimationLibrary animationLibrary;
    
    private Dictionary<string, AnimationSequence> _animations = new Dictionary<string, AnimationSequence>();
    
    private string _currentAnimationName;
    private Coroutine _currentAnimationCoroutine;

    private void Start()
    {
        foreach (AnimationSequence animation in animationLibrary.sequences)
        {
            _animations[animation.name] = animation;
        }
        
        PlayAnimation("Idle");
    }
    
    public void ChangeState(Vector2 movement)
    {
        if (movement == Vector2.zero)
        {
            PlayAnimation("Idle");
        }
        else if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
        {
            if (movement.x > 0)
            {
                PlayAnimation("Right");
            }
            else
            {
                PlayAnimation("Left");
            }
        }
        else
        {
            if (movement.y > 0)
            {
                PlayAnimation("Up");
            }
            else
            {
                PlayAnimation("Down");
            }
        }
    }
    
    public void PlayAnimation(string animationName)
    {
        if (animationName == _currentAnimationName)
        {
            return;
        }
        
        AnimationSequence animationSequence;
        if (!_animations.TryGetValue(animationName, out animationSequence))
        {
            return;
        }

        if(_currentAnimationCoroutine != null) StopCoroutine(_currentAnimationCoroutine);
        _currentAnimationCoroutine = StartCoroutine(Animate(animationSequence));
    }

    private IEnumerator Animate(AnimationSequence animationSequence)
    {
        _currentAnimationName = animationSequence.name;
        
        var frameTime = 1f / animationSequence.frameRate;
        
        var frameIndex = 0;
        var delay = new WaitForSeconds(frameTime);
        while (true)
        {
            spriteRenderer.sprite = animationSequence.frames[frameIndex];
            
            yield return delay;
            
            frameIndex++;
            
            if (frameIndex >= animationSequence.frames.Length)
            {
                if (animationSequence.loop)
                {
                    frameIndex = 0;
                }
                else
                {
                    _currentAnimationName = null;
                    yield break;
                }
            }
        }
    }
}
