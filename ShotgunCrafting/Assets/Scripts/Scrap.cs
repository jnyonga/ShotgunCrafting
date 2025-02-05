using UnityEngine;

public class Scrap : MonoBehaviour
{
    [SerializeField] int scrapAmount = 25;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Collected Scrap");
            if (collision.gameObject.GetComponent<PlayerResources>() != null)
            {
                collision.gameObject.GetComponent<PlayerResources>().AddScrap(scrapAmount);
            }
            
            Destroy(gameObject);
        }
    }
}
