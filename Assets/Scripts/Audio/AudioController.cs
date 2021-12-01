using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AudioController : Singleton<AudioController>
{
    public AudioClipsData data = null;
    private List<AudioSource> audioSources = new List<AudioSource>();

    public void Start()
    {
        audioSources = GetComponentsInChildren<AudioSource>().ToList();
        PlaySingle(data.levelMusic, true);
    }

    void StopAllSFX()
    {
        foreach (AudioSource a in audioSources)
            a.Stop();
    }
    AudioSource GetAvailableAudioSource()
    {
        foreach (AudioSource a in audioSources)
        {
            if (!a.isPlaying)
                return a;
        }

        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSources.Add(audioSource);
        return audioSource;
    }

    public int PlaySingle(AudioClip clip, bool loop)
    {

        AudioSource currentSource = GetAvailableAudioSource();
        currentSource.clip = clip;
        currentSource.loop = loop;
        currentSource.Play();
        return audioSources.IndexOf(currentSource);
    }
    public void Stop(int index) => audioSources[index].Stop();
}

