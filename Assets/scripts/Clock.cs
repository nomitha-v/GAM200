using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public static bool gameOngoing;
    public float startingTime;
    public float endTime;
    float elapsedTime;

    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] Canvas endRoundScreen;

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
            timer.text = string.Format("{0:00}:{1:00}", min, sec);

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
    }
}
