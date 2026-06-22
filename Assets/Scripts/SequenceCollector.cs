using UnityEngine;

[System.Serializable]
public class SequenceElement
{
    public GameObject numberObject;
    public AudioClip uniqueSound;
}

public class SequenceCollector : MonoBehaviour
{
    [Header("Drop your objects AND their unique sounds here!")]
    public SequenceElement[] sequenceElements;

    [Header("VFX Triggers")]
    public VFXMilestoneTrigger milestoneTrigger;

    private int currentIndex = 0;

    [Header("General Audio Settings")]
    public AudioSource audioSource;
    public AudioClip errorSound;

    void Start()
    {
        foreach (SequenceElement element in sequenceElements)
        {
            if (element.numberObject != null)
            {
                SequenceItemHelper helper = element.numberObject.AddComponent<SequenceItemHelper>();
                helper.mainManager = this;
            }
        }
    }

    // Notice we now ask for the player GameObject here too!
    public void CheckCollision(GameObject touchedObject, GameObject player)
    {
        if (currentIndex >= sequenceElements.Length) return;

        if (touchedObject == sequenceElements[currentIndex].numberObject)
        {
            Debug.Log("Awesome! Collected: " + touchedObject.name);

            // Play the UNIQUE sound
            AudioClip soundToPlay = sequenceElements[currentIndex].uniqueSound;
            if (audioSource != null && soundToPlay != null)
            {
                audioSource.PlayOneShot(soundToPlay);
            }

            Destroy(touchedObject);
            currentIndex++;

            if (milestoneTrigger != null) milestoneTrigger.CheckCollectionProgress(currentIndex);

            // --- VICTORY STATE ---
            if (currentIndex >= sequenceElements.Length)
            {
                Debug.Log("VICTORY! All objects collected in the perfect sequence!");

                // Grab the animator from the player and trigger isWon!
                Animator playerAnimator = player.GetComponent<Animator>();
                if (playerAnimator != null)
                {
                    playerAnimator.SetBool("isWon", true);
                }
            }
        }
        else
        {
            Debug.Log("Wrong! You need to collect " + sequenceElements[currentIndex].numberObject.name + " first.");

            if (audioSource != null && errorSound != null)
            {
                audioSource.PlayOneShot(errorSound);
            }
        }
    }
}

// -------------------------------------------------------------------
// INVISIBLE HELPER SCRIPT
// -------------------------------------------------------------------

public class SequenceItemHelper : MonoBehaviour
{
    [HideInInspector]
    public SequenceCollector mainManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (mainManager != null)
            {
                // Pass BOTH the number we touched, AND the player who touched it!
                mainManager.CheckCollision(this.gameObject, other.gameObject);
            }
        }
    }
}