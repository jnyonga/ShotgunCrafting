using UnityEngine;
using TMPro;

public class PlayerResources : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scrapText;
    [SerializeField] TextMeshProUGUI rageText;
    [SerializeField] public int currentScrap = 0;
    [SerializeField] public float currentRage = 0f;

    private void Update()
    {
        scrapText.text = currentScrap.ToString();
        rageText.text = currentRage.ToString();

        if (currentRage > 200f)
            currentRage = 200f;

        if (currentRage < 0f)
            currentRage = 0f;
    }
    private void FixedUpdate()
    {
        currentRage -= Time.deltaTime * 2;
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
