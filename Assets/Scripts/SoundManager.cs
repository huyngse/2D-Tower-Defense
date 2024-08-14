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
        LoadVolume();
        musicSlider
            .onValueChanged
            .AddListener(
                delegate
                {
                    UpdateVolume();
                }
            );
        sfxSlider
            .onValueChanged
            .AddListener(
                delegate
                {
                    UpdateVolume();
                }
            );
    }

    public void PlayEffect(string name)
    {
        sfxSource.PlayOneShot(audioClips[name]);
    }

    public void UpdateVolume()
    {
        musicSource.volume = musicSlider.value;
        sfxSource.volume = sfxSlider.value;
        PlayerPrefs.SetFloat("music", musicSource.volume);
        PlayerPrefs.SetFloat("SFX", sfxSource.volume);
    }

    public void LoadVolume()
    {
        musicSource.volume = PlayerPrefs.GetFloat("music", 0.5f);
        sfxSource.volume = PlayerPrefs.GetFloat("SFX", 0.5f);
        musicSlider.value = musicSource.volume;
        sfxSlider.value = sfxSource.volume;
    }
}
