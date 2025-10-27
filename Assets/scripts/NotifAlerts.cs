using TMPro;
using UnityEngine;

public class NotifAlerts : MonoBehaviour
{
    [SerializeField] Color hungerColor;
    [SerializeField] Color healthColor;
    [SerializeField] Color sanityColor;

    [SerializeField] TextMeshProUGUI alertBox;

    public bool HungerAlert(string animal)
    {
        //putting in the text
        string alert = string.Format("<b><color=#{0}>{1} </b><color=#FFFFFF>is running low in {2} Enclosure", ColorUtility.ToHtmlStringRGB(hungerColor), "FOOD BAR", animal);
        alertBox.text += "\r\n" + alert;


        return true;
    }
    public bool HealthAlert(string animal)
    {
        //putting in the text
        string alert = string.Format("<b><color=#{0}>{1} </b><color=#FFFFFF>is running low in {2} Enclosure", ColorUtility.ToHtmlStringRGB(healthColor), "HEALTH BAR", animal);
        alertBox.text += "\r\n" + alert;

        return true;
    }
    public bool SanityAlert(string animal)
    {
        //putting in the text
        string alert = string.Format("<b><color=#{0}>{1} </b><color=#FFFFFF>is running low in {2} Enclosure", ColorUtility.ToHtmlStringRGB(sanityColor), "SANITY BAR", animal);
        alertBox.text += "\r\n" + alert;

        return true;
    }
}
