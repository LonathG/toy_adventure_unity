using UnityEngine;

public class HintZone : MonoBehaviour
{
    [Header("Unique Sound for This Specific Hint")]
    public AudioClip customHintSound;

    private HintSequenceManager hintManager;

    void Start()
    {
        // Automatically find our hint sequence manager in the scene
        hintManager = Object.FindFirstObjectByType<HintSequenceManager>();

        if (hintManager == null)
        {
            Debug.LogError("HintZone: Could not locate a HintSequenceManager in the scene!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the object stepping into the circle zone is tagged "Player"
        if (other.CompareTag("Player"))
        {
            if (hintManager != null)
            {
                // Send this specific object and its custom sound over to the manager
                hintManager.OnHintOverlapped(gameObject, customHintSound);
            }
        }
    }
}