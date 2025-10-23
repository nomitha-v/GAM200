using UnityEngine;
using UnityEngine.UI;

public class InventoryClickUI : MonoBehaviour
{
    [Header("UI References")]
    public GameObject messageObject;   // Assign a Text or TMP object in Inspector
    public float messageDuration = 2f; // Time in seconds the message stays

    private void Start()
    {
        if (messageObject != null)
            messageObject.SetActive(false); // Hide at start
    }

    public void OnClick()
    {
        // Show message when clicked
        if (messageObject != null)
        {
            StopAllCoroutines();
            StartCoroutine(ShowMessage());
        }
    }

    private System.Collections.IEnumerator ShowMessage()
    {
        messageObject.SetActive(true);
        yield return new WaitForSeconds(messageDuration);
        messageObject.SetActive(false);
    }
}