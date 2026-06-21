using UnityEngine;
using UnityEngine.UI; // Required for controlling UI Elements

public class ToyManager : MonoBehaviour
{
    public int totalToysToCollect = 4;
    private int currentToysCollected = 0;

    [Header("UI Icons (Assign Toy1 to Toy4 here)")]
    public Image[] toyIcons;
    public Color collectedColor = Color.white; // Full color tint

    [Header("Audio Sources")]
    public AudioSource collectionAudioSource;
    public AudioSource victoryAudioSource;

    [Header("Sound Clips")]
    public AudioClip pickupSound;
    public AudioClip victorySound;

    public void CollectToy()
    {
        if (currentToysCollected < totalToysToCollect)
        {
            // 1. Change the color of the corresponding icon to full color
            if (toyIcons != null && currentToysCollected < toyIcons.Length)
            {
                toyIcons[currentToysCollected].color = collectedColor;
            }

            currentToysCollected++;
            Debug.Log("Toys Collected: " + currentToysCollected + "/" + totalToysToCollect);

            // 2. Play standard pickup audio
            if (currentToysCollected < totalToysToCollect)
            {
                if (collectionAudioSource && pickupSound)
                    collectionAudioSource.PlayOneShot(pickupSound);
            }
            // 3. Play victory music on final item
            else if (currentToysCollected == totalToysToCollect)
            {
                if (victoryAudioSource && victorySound)
                    victoryAudioSource.PlayOneShot(victorySound);

                TriggerVictory();
            }
        }
    }

    void TriggerVictory()
    {
        Debug.Log("Victory! All toys collected!");
    }
}