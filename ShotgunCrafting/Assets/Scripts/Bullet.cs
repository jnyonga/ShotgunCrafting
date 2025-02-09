using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] float bulletSpeed;
    [SerializeField] float damage;
    [SerializeField] float bulletDuration;
    private Transform target;

    private void Update()
    { 
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player Hit");
            PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
