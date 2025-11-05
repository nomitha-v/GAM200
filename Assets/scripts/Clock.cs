using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public static Clock Instance;

    public static bool gameOngoing;
    public float startingTime;
    public float endTime;
    float elapsedTime;

    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] Canvas endRoundScreen;

    [SerializeField] TextMeshProUGUI dieReason;
    [SerializeField] Canvas dieScreen;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOngoing)
        {
            elapsedTime += Time.deltaTime;
            int min = Mathf.FloorToInt(elapsedTime / 60);
            int sec = Mathf.FloorToInt(elapsedTime % 60);
            int millisec = Mathf.FloorToInt((elapsedTime*100)%100);
            timer.text = string.Format("{0:00}:{1:00}:{2:00}", min, sec, millisec);

            if (elapsedTime > endTime)
            {
                HitsMorning();
            }
        }
    }

    public void HitsMorning()
    {
        gameOngoing = false;
        //win screen
        endRoundScreen.gameObject.SetActive(true);
        SoundManager.PlaySound(SoundType.DAYCOMPLETE);
    }

    public void HungerDeath(string animal)
    {
        gameOngoing = false;
        //putting in the text
        dieReason.text = string.Format("{0} starved to death.", animal);
        dieScreen.gameObject.SetActive(true);
    }
    public void HealthDeath(string animal)
    {
        gameOngoing = false;
        //putting in the text
        dieReason.text = string.Format("{0} succumbed to an unknown illness", animal); ;
        dieScreen.gameObject.SetActive(true);
    }
    public void SanityDeath(string animal)
    {
        gameOngoing = false;
        //putting in the animal
        dieReason.text = string.Format("{0} was driven to insanity", animal);
        dieScreen.gameObject.SetActive(true);
    }
}
