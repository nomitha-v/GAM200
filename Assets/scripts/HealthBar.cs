using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("Resource Settings")]
    public float maxValue = 100f;          // Max bar value
    public float currentValue = 50f;       // Starting value
    public float restoreAmount = 25f;      // Amount restored when button is pressed
    public float restoreCooldown = 1f;     // Seconds between restores

    private float restoreTimer;

    [Header("Drain Settings")]
    public float drainPerSecond = 1f;      // Amount drained per second
    public bool drainOnStart = true;

    [Header("UI References")]
    public Slider resourceSlider;
    public Button restoreButton;
    public Text valueText;  // Optional

    public Color onButtonColor;
    public Color offButtonColor;

    public string location;
    public NotifAlerts alertHub;
    public bool notified = false;


    private void Start()
    {
        restoreTimer = restoreCooldown;
        Debug.Log(restoreTimer);
        // Setup slider
        if (resourceSlider != null)
        {
            resourceSlider.maxValue = maxValue;
            resourceSlider.value = currentValue;
        }

        // Hook up button
        if (restoreButton != null)
            restoreButton.onClick.AddListener(RestoreResource);
    }

    private void Update()
    {
        if (Clock.gameOngoing)
        {
            // Drain over time
            if (drainOnStart && currentValue > 0)
            {
                currentValue -= drainPerSecond * Time.deltaTime;
                currentValue = Mathf.Clamp(currentValue, 0, maxValue);
                UpdateUI();

                //if its running low
                if (!notified && currentValue / maxValue <= .15f)
                {
                    notified = alertHub.HealthAlert(location);
                }
                else if (notified && currentValue / maxValue > .15f)
                {
                    notified = false;
                }
            }

            if (restoreTimer >= restoreCooldown)
            {
                Debug.Log(restoreTimer);
                restoreButton.enabled = true;
                restoreButton.image.color = onButtonColor;
            }
            else
            {
                restoreButton.enabled = false;
                restoreButton.image.color = offButtonColor;
                restoreTimer += Time.deltaTime;
                Debug.Log(restoreTimer);
            }
        }
    }

    void RestoreResource()
    {
        currentValue += restoreAmount;
            currentValue = Mathf.Clamp(currentValue, 0, maxValue);
            UpdateUI();
        restoreTimer = 0;

    }

    void UpdateUI()
    {
        if (resourceSlider != null)
            resourceSlider.value = currentValue;

        if (valueText != null)
            valueText.text = $"{(int)currentValue}/{maxValue}";
    }
}