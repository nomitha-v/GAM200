using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("Resource Settings")]
    public float maxValue = 100f;          // Max bar value
    public float currentValue = 50f;       // Starting value
    public float restoreAmount = 25f;      // Amount restored when button is pressed
    public float restoreCooldown = 1f;     // Seconds between restores
    private float lastRestoreTime = 0f;

    private float restoreTimer;

    [Header("Drain Settings")]
    public float drainPerSecond = 1f;      // Amount drained per second
    public bool drainOnStart = true;

    [Header("UI References")]
    public Slider resourceSlider;
    public Button restoreButton;
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

        // Hook up button
        if (restoreButton != null)
        {
            //buttonText = restoreButton.GetComponentInChildren<TextMeshProUGUI>();
            restoreButton.onClick.AddListener(RestoreResource);
        }
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
                    notified = alertHub.HealthAlert(animal);
                }
                else if (notified && currentValue / maxValue > .15f)
                {
                    notified = false;
                }
            }

            if (restoreTimer >= restoreCooldown)
            {
                restoreButton.enabled = true;
                restoreButton.image.color = onButtonColor;
                restoreButton.GetComponentInChildren<TextMeshProUGUI>().text = "HEALTH";
            }
            else
            {
                restoreButton.enabled = false;
                restoreButton.image.color = offButtonColor;
                restoreTimer += Time.deltaTime;
                int a = Mathf.FloorToInt(restoreCooldown - restoreTimer);
                restoreButton.GetComponentInChildren<TextMeshProUGUI>().text = string.Format ("{0}s", a);
            }

            if (currentValue <= 0)
            {
                Clock.Instance.SanityDeath(animal);
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