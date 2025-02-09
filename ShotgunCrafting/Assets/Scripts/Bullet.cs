using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] float damage;

    private void Update()
    { 
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player Hit");
            
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            PlayerResources playerResources = other.gameObject.GetComponent<PlayerResources>();

            //player loses health
            playerHealth.TakeDamage(damage);

            //player loses scrap
            playerResources.LoseScrap();

            //player loses rage
            playerResources.LoseRage();

            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
        
    }
}
