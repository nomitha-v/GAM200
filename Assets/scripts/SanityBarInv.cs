using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SanityBarInv : MonoBehaviour
{
    [Header("Resource Settings")]
    public float maxValue = 100f;          // Max bar value
    public float currentValue = 50f;       // Starting value
    public float changeAmount = 25f;       // Amount DECREASED when button is pressed (was restoreAmount)
    public float restoreCooldown = 3f;     // Seconds between button uses
    private float restoreTimer;

    [Header("Fill Settings")]
    public float fillPerSecond = 1f;       // Amount filled per second (was drainPerSecond)
    public bool fillOnStart = true;        // Auto-fill on start (was drainOnStart)

    [Header("UI References")]
    public Slider resourceSlider;
    public Button actionButton;
    public Text valueText;  // Optional
    public TextMeshProUGUI buttonText;

    public Color onButtonColor;
    public Color offButtonColor;

    public string animal;
    public NotifAlerts alertHub;
    public bool notified = false;

    private void Start()
    {
        restoreTimer = restoreCooldown;

        // Setup slider
        if (resourceSlider != null)
        {
            resourceSlider.maxValue = maxValue;
            resourceSlider.value = currentValue;
        }

        // Hook up button (now decreases)
        if (actionButton != null)
            actionButton.onClick.AddListener(DecreaseResource);
    }

    private void Update()
    {
        if (Clock.gameOngoing)
        {
            // Fill over time until max
            if (fillOnStart && currentValue < maxValue)
            {
                currentValue += fillPerSecond * Time.deltaTime;
                currentValue = Mathf.Clamp(currentValue, 0, maxValue);
                UpdateUI();

                /*
                // Notify if running low (kept as-is)
                if (!notified && currentValue / maxValue <= .15f)
                {
                    notified = alertHub.HungerAlert(animal);
                }
                else if (notified && currentValue / maxValue > .15f)
                {
                    notified = false;
                }
                */
            }

            // Button cooldown / label
            if (restoreTimer >= restoreCooldown)
            {
                actionButton.enabled = true;
                actionButton.image.color = onButtonColor;
                actionButton.GetComponentInChildren<TextMeshProUGUI>().text = "SANITY";
            }
            else
            {
                actionButton.enabled = false;
                actionButton.image.color = offButtonColor;
                restoreTimer += Time.deltaTime;
                int a = Mathf.FloorToInt(restoreCooldown - restoreTimer);
                actionButton.GetComponentInChildren<TextMeshProUGUI>().text = string.Format("{0}s", a);
            }

            // Death condition unchanged
            if (currentValue <= 0)
            {
                Clock.Instance.SanityDeath(animal);
            }
        }
    }

    // Button now DECREASES the bar
    void DecreaseResource()
    {
        currentValue -= changeAmount;
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
