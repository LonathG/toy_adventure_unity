using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target to Follow")]
    public Transform target; // Drag your Toddler Player here

    [Header("Fixed Position Offset")]
    public Vector3 offset = new Vector3(0f, 4f, -6f); // X=Left/Right, Y=Up/Down, Z=Back

    [Header("Smoothness")]
    public float positionSmoothSpeed = 0.125f; // How smoothly it slides to catch up

    private Quaternion fixedRotation;

    void Start()
    {
        // Save the exact rotation you set in the Unity Editor so it never drifts
        fixedRotation = transform.rotation;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // 1. Calculate a static world-space position relative to the player
        Vector3 desiredPosition = target.position + offset;

        // 2. Smoothly slide the camera to that position (No rotating/spinning!)
        transform.position = Vector3.Lerp(transform.position, desiredPosition, positionSmoothSpeed);

        // 3. Keep the camera locked to your preferred editor rotation
        transform.rotation = fixedRotation;
    }
}