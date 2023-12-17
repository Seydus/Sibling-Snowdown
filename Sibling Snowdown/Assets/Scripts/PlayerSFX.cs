using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    [Header("Footstep")]
    public AudioClip[] footstepClipList;
    public AudioSource footStepAudioSource;

    [Header("Hit")]
    public AudioClip[] hitClipList;
    public AudioSource hitAudioSource;

    [Header("Snowball Punch")]
    public AudioClip[] snowballPunchClipList;
    public AudioSource snowballPunchAudioSource;

    [Header("Snowball Throw")]
    public AudioClip[] snowballThrowClipList;
    public AudioSource snowballThrowAudioSource;

    private int GetRandomIndex(AudioClip[] clipList)
    {
        return Random.Range(0, clipList.Length);
    }

    public void PlayFootstep()
    {
        if(!footStepAudioSource.isPlaying)
        {
            footStepAudioSource.clip = footstepClipList[GetRandomIndex(footstepClipList)];
            footStepAudioSource.Play();
        }
    }

    public void PlayHit()
    {
        int randomIndex = GetRandomIndex(hitClipList);
        hitAudioSource.PlayOneShot(hitClipList[randomIndex]);
    }

    public void PlaySnowballPunch()
    {
        int randomIndex = GetRandomIndex(snowballPunchClipList);
        snowballPunchAudioSource.PlayOneShot(snowballPunchClipList[randomIndex]);
    }

    public void PlaySnowballThrow()
    {
        int randomIndex = GetRandomIndex(snowballThrowClipList);
        snowballThrowAudioSource.PlayOneShot(snowballThrowClipList[randomIndex]);
    }
}
