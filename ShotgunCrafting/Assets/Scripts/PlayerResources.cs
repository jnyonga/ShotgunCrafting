using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerResources : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scrapText;
    [SerializeField] Image rageBar;
    [SerializeField] public int currentScrap = 0;
    [SerializeField] public int scraptLostOnHit = 10;
    [SerializeField] public float currentRage = 0f;
    float maxRage = 200f;

    private void Start()
    {
        currentRage = 0f;
    }
    private void Update()
    {
        scrapText.text = currentScrap.ToString();
        RageBarFiller();

        if (currentRage > maxRage)
            currentRage = maxRage;

        if (currentRage < 0f)
            currentRage = 0f;

        if (currentScrap < 0)
            currentScrap = 0;
    }
    private void FixedUpdate()
    {
        currentRage -= Time.deltaTime * 2;
    }
    public void AddScrap(int scrap)
    {
        currentScrap += scrap;
    }

    public void LoseScrap()
    {
        currentScrap -= scraptLostOnHit;
    }
    public void AddRage(float rage)
    {
        currentRage += rage;
    }
    public void LoseRage()
    {
        currentRage -= currentRage * (1 - 0.15f);
    }
    void RageBarFiller()
    {
        rageBar.fillAmount = currentRage / maxRage;
    }
}
