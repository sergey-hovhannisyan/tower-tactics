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
    public AudioSource parallelAudioSource;
    [SerializeField] NamedAudioClip[] _audioClips;
    private bool _isMainAudioSourcePaused = false;
    private bool _isParallelAudioSourcePaused = false;

    // Background music
    public void PlayMenuBackgroundMusic()
    {
        PlayAudioClip(0, true);
    }

    public void PlayGameBackgroundMusic()
    {
        PlayAudioClip(1, true);
    }

    public void PlayErrorSound()
    {
        PlayParallelAudioClip(2);
    }

    private void PlayAudioClip(int clipIndex, bool loop)
    {
        if (_isMainAudioSourcePaused)
            return;
        audioSource.loop = loop;
        audioSource.clip = _audioClips[clipIndex].audioClip;
        audioSource.volume = 0.5f;
        audioSource.Play();
    }

    private void PlayParallelAudioClip(int clipIndex)
    {
        if (!_isParallelAudioSourcePaused)
            return;
        if (!parallelAudioSource.isPlaying)
        {
            parallelAudioSource.clip = _audioClips[clipIndex].audioClip;
            parallelAudioSource.volume = 0.5f;
            parallelAudioSource.Play();
        }

    }

    public void StopAudio()
    {
        audioSource.Stop();
        parallelAudioSource.Stop();
    }

    public void PauseAudio()
    {
        audioSource.Pause();
        parallelAudioSource.Pause();
        _isMainAudioSourcePaused = true;
        _isParallelAudioSourcePaused = true;
    }

    public void ResumeAudio()
    {
        audioSource.UnPause();
        parallelAudioSource.UnPause();
        _isMainAudioSourcePaused = false;
        _isParallelAudioSourcePaused = false;
    }
}
