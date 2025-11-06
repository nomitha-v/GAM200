using System;
using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public static Clock Instance;

    public static bool gameOngoing;
    public float startingTime;
    public float endTime;
    public float song2ChangeTime;
    float elapsedTime;

    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] Canvas endRoundScreen;

    [SerializeField] TextMeshProUGUI dieReason;
    [SerializeField] Canvas dieScreen;

    public MusicController musicController;
    public AmbianceController ambianceController;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        elapsedTime = startingTime;
        gameOngoing = true;
        musicController.songIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOngoing)
        {
            elapsedTime += Time.deltaTime;
            Debug.Log($"elapsedTime: {elapsedTime}");

            int min = Mathf.FloorToInt(elapsedTime / 60);
            int sec = Mathf.FloorToInt(elapsedTime % 60);
            int millisec = Mathf.FloorToInt((elapsedTime*100)%100);
            timer.text = string.Format("{0:00}:{1:00}:{2:00}", min, sec, millisec);

            //SoundManager.PlaySound(SoundType.DAYSTART);

            //if(elapsedTime == 5)
            //{
            //    ChangeSong1();
            //}

            if (elapsedTime > endTime)
            {
                HitsMorning();
                ambianceController.StopMusic();
            }
        }
    }

    void ChangeSong1()
    {
        musicController.songIndex = 1;
        musicController.UpdateAudio();
    }
    public void HitsMorning()
    {
        gameOngoing = false;
        //win screen
        endRoundScreen.gameObject.SetActive(true);
        SoundManager.PlaySound(SoundType.DAYCOMPLETE); //sound
    }

    public void HungerDeath(string animal)
    {
        gameOngoing = false;
        //putting in the text
        dieReason.text = string.Format("{0} starved to death.", animal);
        dieScreen.gameObject.SetActive(true);
        SoundManager.PlaySound(SoundType.DAYINCOMPLETE); //sound
    }
    public void HealthDeath(string animal)
    {
        gameOngoing = false;
        //putting in the text
        dieReason.text = string.Format("{0} succumbed to an unknown illness", animal); ;
        dieScreen.gameObject.SetActive(true);
        SoundManager.PlaySound(SoundType.DAYINCOMPLETE); //sound
    }
    public void SanityDeath(string animal)
    {
        gameOngoing = false;
        //putting in the animal
        dieReason.text = string.Format("{0} was driven to insanity", animal);
        dieScreen.gameObject.SetActive(true);
        SoundManager.PlaySound(SoundType.DAYINCOMPLETE); //sound
    }
}
