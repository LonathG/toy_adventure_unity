using UnityEngine;

public class HintSequenceManager : MonoBehaviour
{
    [Header("Sequential Hint Circles (Assign all 6 in order)")]
    public GameObject[] hintCircles;
    private int currentHintIndex = 0; // Tracks which hint is active

    [Header("Audio Configurations")]
    public AudioSource audioSource; // Audio Source speaker component
    public AudioClip hintReachSound; // Audio clip to play on step-in

    void Start()
    {
        // Setup initial scene state: Turn on Hint #1, keep the other 5 invisible
        UpdateHintVisibility();
    }

    // Called by individual hint circles when the player overlaps them
    public void OnHintOverlapped(GameObject reachedHint, AudioClip customSound = null)
    {
        // Verify the player stepped into the CORRECT active hint circle in the chain
        if (hintCircles != null && currentHintIndex < hintCircles.Length && reachedHint == hintCircles[currentHintIndex])
        {
            // 1. Play the chime sound effect
            if (audioSource != null)
            {
                AudioClip clipToPlay = customSound != null ? customSound : hintReachSound;
                if (clipToPlay != null)
                {
                    audioSource.PlayOneShot(clipToPlay);
                }
            }

            // 2. Advance to the next array spot
            currentHintIndex++;

            // 3. Hide the old circle and reveal the next one
            UpdateHintVisibility();

            Debug.Log("Hint " + currentHintIndex + " completed! Moving to next.");
        }
    }

    void UpdateHintVisibility()
    {
        if (hintCircles == null) return;

        for (int i = 0; i < hintCircles.Length; i++)
        {
            if (hintCircles[i] != null)
            {
                // Activates only the circle matching our active progress index number
                hintCircles[i].SetActive(i == currentHintIndex);
            }
        }
    }
}