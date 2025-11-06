using System.Collections.Generic;
using UnityEngine;

public class AmbianceController : MonoBehaviour
{
    public static AmbianceController instance;
    public static bool isPlaying;

    public AudioSource ambianceSource;
    //public List<AudioClip> ambianceClips = new List<AudioClip>();
    public AudioClip horseClip;
    public AudioClip flamingoClip;
    public AudioClip sealClip;
    public AudioClip owlClip;
    [SerializeField, Range(0, 1)] public float volume;
    float tempVolumeHolder;


    private void Awake()
    {
        instance = this;

    }
    private void Update()
    {
        //StopMusic();
    }
    private void Start()
    {
        isPlaying = true;
        StartAudio();
        HorseAmbiance();
    }

    void StartAudio()
    {
        tempVolumeHolder = volume;
        //volume = 0f;

    }
    public void FlamingoAmbiance()
    {
        ambianceSource.clip = flamingoClip;
        ambianceSource.volume = volume;

        ambianceSource.Play();

    }
    public void SealAmbiance()
    {
        ambianceSource.clip = sealClip;
        ambianceSource.volume = volume;

        ambianceSource.Play();
    }

    public void HorseAmbiance()
    {
        ambianceSource.clip = horseClip;
        ambianceSource.volume = volume;

        ambianceSource.Play();
    }

    public void OwlAmbiance()
    {
        ambianceSource.clip = owlClip;
        ambianceSource.volume = volume;

        ambianceSource.Play();
    }

    public void StopMusic()
    {
        ambianceSource.Stop();
    }
}
