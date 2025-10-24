using TMPro;
using UnityEngine;

public class NotifAlerts : MonoBehaviour
{
    [SerializeField] Color hungerColor;
    [SerializeField] Color healthColor;
    [SerializeField] Color sanityColor;

    [SerializeField] TextMeshProUGUI alertBox;

    public bool HungerAlert(string location)
    {
        //putting in the text
        string alert = string.Format("<b><color=#{0}>{1} </b><color=#FFFFFF>is running low in {2}", ColorUtility.ToHtmlStringRGB(hungerColor), "FOOD BAR", location);
        alertBox.text += "\r\n" + alert;

        return true;
    }
    public bool HealthAlert(string location)
    {
        //putting in the text
        string alert = string.Format("<b><color=#{0}>{1} </b><color=#FFFFFF>is running low in {2}", ColorUtility.ToHtmlStringRGB(healthColor), "HEALTH BAR", location);
        alertBox.text += "\r\n" + alert;

        return true;
    }
    public bool SanityAlert(string location)
    {
        //putting in the text
        string alert = string.Format("<b><color=#{0}>{1} </b><color=#FFFFFF>is running low in {2}", ColorUtility.ToHtmlStringRGB(sanityColor), "SANITY BAR", location);
        alertBox.text += "\r\n" + alert;

        return true;
    }
}
