using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] float health;
    float maxHealth = 100;

    [Header("Object References")]
    [SerializeField] Image healthBar;

    private void Start()
    {
        health = maxHealth;
    }
    private void Update()
    {
        HealthBarFiller();

        if (health <= 0)
            health = 0;

        if (health > maxHealth)
            health = maxHealth;
    }
    void HealthBarFiller()
    {
        healthBar.fillAmount = health/maxHealth;
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    public void GainHealth()
    {
        health += 25f;
    }
}
