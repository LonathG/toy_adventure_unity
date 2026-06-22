using UnityEngine;

public class VFXMilestoneTrigger : MonoBehaviour
{
    [Header("Link ALL your Teleport Zones here!")]
    // Notice the brackets [] - this makes it a list!
    public TeleportZoneVFX[] teleportZones;

    // This gets called every time you pick up a correct number
    public void CheckCollectionProgress(int currentScore)
    {
        // If we just collected number 3
        if (currentScore == 3)
        {
            // Loop through every teleport zone in our list and turn them on!
            foreach (TeleportZoneVFX zone in teleportZones)
            {
                if (zone != null) zone.ShowFirstVFX();
            }
        }
        // If we just collected number 7
        else if (currentScore == 7)
        {
            foreach (TeleportZoneVFX zone in teleportZones)
            {
                if (zone != null) zone.ShowSecondVFX();
            }
        }
    }
}