using UnityEngine;

public class TeleportZoneVFX : MonoBehaviour
{
    [Header("Drag your Particle Systems here")]
    public GameObject phase1VFX; // The VFX that shows after item 3
    public GameObject phase2VFX; // The VFX that shows after item 7

    void Start()
    {
        // Ensure the VFX are completely turned off when the game starts
        if (phase1VFX != null) phase1VFX.SetActive(false);
        if (phase2VFX != null) phase2VFX.SetActive(false);
    }

    // This is called automatically after you collect 1, 2, and 3
    public void ShowFirstVFX()
    {
        if (phase1VFX != null) phase1VFX.SetActive(true);
        Debug.Log("Teleport Zone: Phase 1 VFX Activated!");
    }

    // This is called automatically after you collect 4, 5, 6, and 7
    public void ShowSecondVFX()
    {
        // Turn off the first VFX, and turn on the second bigger/better one
        if (phase1VFX != null) phase1VFX.SetActive(false);
        if (phase2VFX != null) phase2VFX.SetActive(true);
        Debug.Log("Teleport Zone: Phase 2 VFX Activated!");
    }
}