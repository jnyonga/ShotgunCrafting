using UnityEngine;
using TMPro;

public class PlayerResources : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scrapText;
    [SerializeField] public int currentScrap = 0;
    [SerializeField] float currentRage = 0f;

    private void Update()
    {
        scrapText.text = currentScrap.ToString();
    }
    public void AddScrap(int scrap)
    {
        currentScrap += scrap;
    }

    public void AddRage(float rage)
    {
        currentRage += rage;
    }
}
