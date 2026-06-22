using UnityEngine;

public class HoverAndBounce : MonoBehaviour
{
    [Header("Hover Settings")]
    public float bounceSpeed = 2f;    // How fast the object bounces
    public float bounceHeight = 0.5f; // How high the object bounces

    private float startY;
    private float randomOffset;

    void Start()
    {
        // Store the initial Y position so it bounces around its starting point
        startY = transform.position.y;

        // Create a random offset so multiple objects don't bounce exactly in sync
        randomOffset = Random.Range(0f, 2f * Mathf.PI);
    }

    void Update()
    {
        // Calculate the new Y position using a Sine wave
        float newY = startY + Mathf.Sin(Time.time * bounceSpeed + randomOffset) * bounceHeight;

        // Apply the new position while keeping X and Z exactly the same
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}