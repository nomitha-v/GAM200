using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
#if TMP_PRESENT
using TMPro;
#endif
using System.Collections;

public class DropZone : MonoBehaviour, IDropHandler
{
    [Header("Message settings")]
    public GameObject messageObject; // optional: drag a Text or TMP_Text GameObject (set inactive by default)
    public float messageDuration = 1.5f;
    public string messageText = "Inventory stocked";

    // optional: if using Unity UI Text
    public Text uiText;
#if TMP_PRESENT
    public TMP_Text tmpText;
#endif

    // Optional: whether to accept a specific itemID; leave empty to accept any
    public string acceptedItemID = "";

    // Optional: reference to an InventoryManager to actually add the item
    // public InventoryManager inventoryManager;

    void Start()
    {
        if (messageObject != null) messageObject.SetActive(false);
    }

    public void OnDrop(PointerEventData eventData)
    {
        // Check if the thing being dragged has DraggableItem
        var dragged = eventData.pointerDrag;
        if (dragged == null) return;

        var draggable = dragged.GetComponent<DraggableItem>();
        if (draggable == null) return;

        // If we only accept a certain itemID, check it
        if (!string.IsNullOrEmpty(acceptedItemID) && draggable.itemID != acceptedItemID)
        {
            // optionally show "can't place" feedback
            return;
        }

        // Optionally: re-parent the dragged item to the drop zone (to visually place it in the box)
        dragged.transform.SetParent(this.transform, false);

        // Optionally: snap to center
        var rt = dragged.GetComponent<RectTransform>();
        if (rt != null) rt.anchoredPosition = Vector2.zero;

        // Optionally: Add to inventory system:
        // if (inventoryManager != null) inventoryManager.AddItem(draggable.itemID, 1);

        // Show message
        ShowMessage();
    }

    private void ShowMessage()
    {
        if (messageObject != null)
        {
            // If a messageObject is provided, set its text (if needed) and show it
            if (uiText != null) uiText.text = messageText;
#if TMP_PRESENT
            if (tmpText != null) tmpText.text = messageText;
#endif

            StopAllCoroutines();
            StartCoroutine(ShowMessageRoutine());
        }
        else
        {
            // Fallback: Debug log
            Debug.Log(messageText);
        }
    }

    private IEnumerator ShowMessageRoutine()
    {
        messageObject.SetActive(true);
        yield return new WaitForSeconds(messageDuration);
        messageObject.SetActive(false);
    }
}