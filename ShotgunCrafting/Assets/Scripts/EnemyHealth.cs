using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 20;
    [SerializeField] GameObject scrap;

   public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0 )
        {
            Instantiate(scrap, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
