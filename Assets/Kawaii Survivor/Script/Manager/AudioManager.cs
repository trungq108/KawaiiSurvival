using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    private AudioClip[] musicClips;
    private AudioClip[] sfxClips;

    private Dictionary<string, AudioClip> musicDict = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> sfxDict = new Dictionary<string, AudioClip>();

    private void OnEnable()
    {
        OnInit();
        GameEvent.onRangeWeaponAttack += PlaySFX;
        GameEvent.onMeleeWeaponAttack += PlaySFX;

        PlayMusic("GameMusic");
    }

    private void OnDisable()
    {
        GameEvent.onRangeWeaponAttack -= PlaySFX;
        GameEvent.onMeleeWeaponAttack -= PlaySFX;
    }

    private void OnInit()
    {
        musicClips = Resources.LoadAll<AudioClip>("Audio/Music");
        sfxClips = Resources.LoadAll<AudioClip>("Audio/SFX");

        foreach (AudioClip clip in musicClips)
        {
            musicDict[clip.name] = clip;
        }

        foreach (AudioClip clip in sfxClips)
        {
            sfxDict[clip.name] = clip;
        }
    }

    public void PlayMusic(string musicName)
    {
        if (musicDict.TryGetValue(musicName, out AudioClip clip))
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning($"Music clip with name {musicName} not found!");
        }
    }

    public void PlaySFX(string sfxName)
    {
        if (sfxDict.TryGetValue(sfxName, out AudioClip clip))
        {
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"SFX clip with name {sfxName} not found!");
        }
    }
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void StopMusic() => musicSource.Stop();

    public static void StopSFX() => Instance.sfxSource.Stop();

    public void SetMusicVolume(float volume) => musicSource.volume = volume;

    public void SetSFXVolume(float volume) => sfxSource.volume = volume;

}
