using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : Singleton<AudioController>
{
    [Header("Volume")]
    [Range(0f, 1f)]
    [SerializeField] float musicVolume;
    [Range(0f, 1f)]
    [SerializeField] float sfxVolume;

    public AudioSource musicAudio;
    public AudioSource sfxAudio;

    [Header("Game Sounds and Musics")]
    public AudioClip shooting;
    public AudioClip win;
    public AudioClip lose;
    public AudioClip[] bgMusics;

    private void Start()
    {
        PlayMusic(bgMusics);
    }

    public void PlaySound(AudioClip sound, AudioSource aus = null)
    {
        if (!aus)
        {
            aus = sfxAudio;
        }

        if (aus)
        {
            aus.PlayOneShot(sound, sfxVolume);
        }
    }

    public void PlaySound(AudioClip[] sounds, AudioSource aus = null)
    {
        if (!aus)
        {
            aus = sfxAudio;
        }
        if (aus)
        {
            int random = Random.Range(0, sounds.Length);

            if (sounds[random] != null)
            {
                aus.PlayOneShot(sounds[random], sfxVolume);
            }
        }
    }

    public void PlayMusic(AudioClip music, bool loop = true)
    {
        if (musicAudio)
        {
            musicAudio.clip = music;
            musicAudio.loop = loop;
            musicAudio.volume = sfxVolume;
            musicAudio.Play();
        }
    }
    public void PlayMusic(AudioClip[] musics, bool loop = true)
    {
        if (musicAudio)
        {
            int random = Random.Range(0, musics.Length);

            if (musics[random] != null)
            {
                musicAudio.clip = musics[random];
                musicAudio.loop = loop;
                musicAudio.volume = sfxVolume;
                musicAudio.Play();
            }
        }
    }
}
