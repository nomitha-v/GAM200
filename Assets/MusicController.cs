using UnityEngine;
using System.Collections.Generic;
using System;
public class MusicController : MonoBehaviour
{
    public static MusicController instance;
    public static bool isPlaying;

    public AudioSource bgmSource;
    public List<AudioClip> bgmClips = new List<AudioClip>();
    public int songIndex;
    [SerializeField, Range(0, 1)] public float volume;
    float tempVolumeHolder;


    private void Awake()
    {
        instance = this;
       
    }
    private void Update()
    {
        StopMusic();
    }
    private void Start()
    {
        isPlaying = true;
        StartAudio();
    }

    void StartAudio()
    {
        tempVolumeHolder = volume;
        volume = 0f;

        bgmSource.clip = bgmClips[songIndex];
        bgmSource.volume = tempVolumeHolder;
        bgmSource.Play();
    }
    /// <summary>
    /// thsi can call if the time hits a certain time and just update this
    /// </summary>
    public void UpdateAudio()
    {
        bgmSource.clip = bgmClips[songIndex];
        bgmSource.Play();
    }

    public void StopMusic()
    {
        if(Clock.gameOngoing == false)
        {
            bgmSource.Stop();
        }
    }
}
