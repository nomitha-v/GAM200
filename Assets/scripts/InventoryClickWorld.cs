using UnityEngine;
using UnityEngine.UI;

public class InventoryClickWorld : MonoBehaviour
{
    [Header("UI References")]
    public GameObject messageObject;   // Drag your text object here
    public float messageDuration = 2f;

    private void Start()
    {
        if (messageObject != null)
            messageObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        // This works if the sprite has a Collider2D
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