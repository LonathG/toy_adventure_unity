using UnityEngine;

public class ToyItem : MonoBehaviour
{
    private ToyManager toyManager;

    void Start()
    {
        // Find the ToyManager in the scene automatically
        toyManager = Object.FindFirstObjectByType<ToyManager>();

        if (toyManager == null)
        {
            Debug.LogError("Missing a ToyManager GameObject in the scene!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the object touching the toy is the Player
        if (other.CompareTag("Player"))
        {
            // Tell the manager a toy was collected
            if (toyManager != null)
            {
                toyManager.CollectToy();
            }

            // Destroy the toy so it disappears from the map
            Destroy(gameObject);
        }
    }
}