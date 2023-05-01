using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NamedAudioClip
{
    public string clipName;
    public AudioClip audioClip;
}

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    [SerializeField] NamedAudioClip[] _audioClips;

    // Background music
    public void PlayMenuBackgroundMusic()
    {
        PlayAudioClip(0, true);
    }

    public void PlayGameBackgroundMusic()
    {
        PlayAudioClip(1, true);
    }

    // Important sound effects
    public void PlayGameStartSound()
    {
        PlayAudioClip(2, false);
    }

    public void PlayGameOverSound()
    {
        PlayAudioClip(3, false);
    }

    public void PlayGameWinSound()
    {
        PlayAudioClip(4, false);
    }

    public void PlayEnemyDeathSound()
    {
        PlayAudioClip(5, false);
    }

    public void PlayNextWaveSound()
    {
        PlayAudioClip(6, false);
    }

    public void MusicOn()
    {
        audioSource.UnPause();
    }

    public void MusicOff()
    {
        audioSource.Pause();
    }

    private void PlayAudioClip(int clipIndex, bool loop)
    {
        audioSource.loop = loop;
        audioSource.clip = _audioClips[clipIndex].audioClip;
        audioSource.Play();
    }
}
