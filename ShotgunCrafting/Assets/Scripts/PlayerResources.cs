using UnityEngine;

public class PlayerResources : MonoBehaviour
{
    [SerializeField] int currentScrap = 0;
    [SerializeField] float currentRage = 0f;

    public void AddScrap(int scrap)
    {
        currentScrap += scrap;
    }

    public void AddRage(float rage)
    {
        currentRage += rage;
    }
}
