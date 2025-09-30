using UnityEngine;
using UnityEngine.UI;

public class ResourceBar : MonoBehaviour
{
    [Header("Resource Settings")]
    public float maxValue = 100f;          // Max bar value
    public float currentValue = 50f;       // Starting value
    public float restoreAmount = 25f;      // Amount restored when button is pressed
    public float restoreCooldown = 1f;     // Seconds between restores
    private float lastRestoreTime = 0f;

    [Header("Drain Settings")]
    public float drainPerSecond = 1f;      // Amount drained per second
    public bool drainOnStart = true;

    [Header("UI References")]
    public Slider resourceSlider;
    public Button restoreButton;
    public Text valueText;  // Optional

    private void Start()
    {
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
        // Drain over time
        if (drainOnStart && currentValue > 0)
        {
            currentValue -= drainPerSecond * Time.deltaTime;
            currentValue = Mathf.Clamp(currentValue, 0, maxValue);
            UpdateUI();
        }
    }

    void RestoreResource()
    {
        if (Time.time - lastRestoreTime >= restoreCooldown)
        {
            currentValue += restoreAmount;
            currentValue = Mathf.Clamp(currentValue, 0, maxValue);
            UpdateUI();

            lastRestoreTime = Time.time;
        }
    }

    void UpdateUI()
    {
        if (resourceSlider != null)
            resourceSlider.value = currentValue;

        if (valueText != null)
            valueText.text = $"{(int)currentValue}/{maxValue}";
    }
}