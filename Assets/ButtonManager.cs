using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject[] animalPanels;
    [SerializeField] private Image[] backgrounds;
    [SerializeField] private Sprite activeSprite, inactiveSprite;
    private int activePanel;
    private void Start()
    {
        SelectPanel(0);
    }
    public void SelectPanel(int panelIndex)
    {
        activePanel = panelIndex;

        SetPanelsActive(false);

        animalPanels[panelIndex].SetActive(true);
        backgrounds[activePanel].sprite = activeSprite;
    }
    private void SetPanelsActive(bool state)
    {
        for (int i = 0; i < animalPanels.Length; i++)
        {
            animalPanels[i].SetActive(state);
            if (state == true)
            {
                backgrounds[i].sprite = activeSprite;
            }
            else
            {
                backgrounds[i].sprite = inactiveSprite;
            }
        }
    }
}
