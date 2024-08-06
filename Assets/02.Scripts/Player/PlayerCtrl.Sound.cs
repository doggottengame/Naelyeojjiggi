using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WalkState
{
    Grass,
    Ice,
    Rock,
    Ground,
    Water,
    Volcano
}
public partial class PlayerCtrl : CharaterCtrl
{
    [SerializeField]
    protected AudioSource AudioSource;
    [SerializeField]
    AudioSource StateSource, waterSource;
    [SerializeField]
    AudioClip[] waterClip;
    [SerializeField]
    AudioClip rollClip, attackClip;
    [SerializeField]
    protected AudioClip burnClip, deathClip;

    void AudioCtrl()
    {
        if (inWater && dir.sqrMagnitude > 0.09f && !waterSource.isPlaying) waterSource.PlayOneShot(waterClip[Random.Range(0, waterClip.Length)]);
    }
}
