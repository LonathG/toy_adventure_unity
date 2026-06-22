using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [Header("Teleport Destination")]
    [Tooltip("Drag the destination empty GameObject here.")]
    public Transform destinationPoint;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the zone is the Player
        if (other.CompareTag("Player"))
        {
            // Move the player to the exact position of the destination point
            other.transform.position = destinationPoint.position;

            // Optional: If you want the player to face the same way the destination point is facing, uncomment the line below
            // other.transform.rotation = destinationPoint.rotation;
        }
    }
}