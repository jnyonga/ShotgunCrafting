using UnityEngine;

public class CollectScrap : MonoBehaviour
{
    [SerializeField] private float collectionRadius = 10f;
    [SerializeField] private float collectionSpeed = 5f;
    [SerializeField] private string scrapTag = "Scrap";

    private void Update()
    {
        ScrapCollect();
    }

    void ScrapCollect()
    {
        Collider[] scrapObjects = Physics.OverlapSphere(transform.position, collectionRadius);

        foreach (Collider col in scrapObjects)
        {
            // Check if the object has the "Scrap" tag
            if (col.CompareTag(scrapTag))
            {
                // Move scrap objects towards the player
                MoveScrapToPlayer(col.gameObject);
            }
        }
    }

    void MoveScrapToPlayer(GameObject scrap)
    {
        // Calculate direction from the scrap object to the player
        Vector3 directionToPlayer = (transform.position - scrap.transform.position).normalized;

        // Move the scrap object towards the player
        scrap.transform.position = Vector3.MoveTowards(scrap.transform.position, transform.position, collectionSpeed * Time.deltaTime);
    }

    // Optional: Visualize the collection radius in the Scene view
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, collectionRadius);
    }
}
