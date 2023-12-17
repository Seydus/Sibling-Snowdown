using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour
{
    public AudioClip mainMusic;
    public AudioClip winningMusic;
    public AudioSource audioSource;

    void Start()
    {
        PlayMainMusic();
    }

    public void PlayMainMusic()
    {
        audioSource.volume = 0.4f;
        audioSource.clip = mainMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayWinningMusic()
    {
        audioSource.loop = false;
        audioSource.Stop();
        audioSource.volume = 0.8f;
        audioSource.PlayOneShot(winningMusic);
    }
}
