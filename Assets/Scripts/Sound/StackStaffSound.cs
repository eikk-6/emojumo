using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackStaffSound : MonoBehaviour
{
    public enum SoundState
    {
        Arance,
        Fire,
        Ice,
        Lightning,
        Nature,
        Positive
    }

    public SoundState state;
    private AudioSource audioSource;
    public AudioClip[] audioClip;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {

        switch(state)
        {
            case SoundState.Arance:
                audioSource.clip = audioClip[0];
                audioSource.Play();
                break;
            case SoundState.Fire:
                audioSource.clip = audioClip[1];
                audioSource.Play();
                break;
            case SoundState.Ice:
                audioSource.clip = audioClip[2];
                audioSource.Play();
                break;
            case SoundState.Lightning:
                audioSource.clip = audioClip[3];
                audioSource.Play();
                break;
            case SoundState.Nature:
                audioSource.clip = audioClip[4];
                audioSource.Play();
                break;
            case SoundState.Positive:
                audioSource.clip = audioClip[5];
                audioSource.Play();
                break;
        }
    }
}
