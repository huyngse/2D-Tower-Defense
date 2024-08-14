using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : Singleton<SoundManager>
{
    [Header("References")]
    [SerializeField]
    private AudioSource musicSource;

    [SerializeField]
    private AudioSource sfxSource;

    [SerializeField]
    private Slider musicSlider;

    [SerializeField]
    private Slider sfxSlider;
    private Dictionary<string, AudioClip> audioClips = new();

    void Start()
    {
        AudioClip[] clips = Resources.LoadAll<AudioClip>("Audio");
        foreach (AudioClip clip in clips)
        {
            audioClips.Add(clip.name, clip);
        }
    }

    public void PlayEffect(string name)
    {
        sfxSource.PlayOneShot(audioClips[name]);
    }
}
