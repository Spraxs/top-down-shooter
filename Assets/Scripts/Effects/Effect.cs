using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    private Animator animator;

    private List<AnimationClip> animationClips = new List<AnimationClip>();

    private ObjectPool objectPool;

    void Awake()
    {
        objectPool = ObjectPool.Instance;
        animator = GetComponent<Animator>();

        animationClips.AddRange(animator.runtimeAnimatorController.animationClips);

        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        gameObject.SetActive(true);

        if (animationClips.Count == 0)
        {
            StopAnimation();
        }

        AnimationClip animationClip = animationClips[Random.Range(0, animationClips.Count)];

        animator.Play(animationClip.name);
    }

    public void StopAnimation()
    {
        objectPool.PoolObject(gameObject);
    }
}
