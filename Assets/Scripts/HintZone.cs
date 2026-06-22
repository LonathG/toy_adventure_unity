using UnityEngine;
using UnityEngine.Audio; // 👈 REQUIRED: Allows the script to access Unity Audio Mixer features

public class HintZone : MonoBehaviour
{
    [Header("🎙️ Voiceover Settings")]
    [Tooltip("Assign the custom voiceover audio file for this specific zone here.")]
    public AudioClip customHintSound;

    [Tooltip("Drag the 'Voiceover' Audio Mixer Group here to enable automatic background music dimming.")]
    public AudioMixerGroup voiceoverMixerGroup; // 👈 NEW: Reference slot for your Ducking audio route

    private HintSequenceManager hintManager;

    void Start()
    {
        // Automatically find our hint sequence manager in the scene
        hintManager = Object.FindFirstObjectByType<HintSequenceManager>();

        if (hintManager == null)
        {
            Debug.LogError("HintZone: Could not locate a HintSequenceManager in the scene!");
        }

        // Safety check to ensure you didn't forget to link the mixer routing group
        if (voiceoverMixerGroup == null)
        {
            Debug.LogWarning($"HintZone on '{gameObject.name}': No Voiceover Mixer Group assigned. BGM dimming will not trigger!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the object stepping into the circle zone is tagged "Player"
        if (other.CompareTag("Player"))
        {
            if (hintManager != null)
            {
                // Send the target zone data and custom sound clip over to the manager
                hintManager.OnHintOverlapped(gameObject, customHintSound);

                // 👉 CRITICAL STEP: Access the AudioSource on the manager or player that plays this clip 
                // and assign its output audio route to our Voiceover Mixer Group.
                AudioSource managerAudioSource = hintManager.GetComponent<AudioSource>();
                if (managerAudioSource != null && voiceoverMixerGroup != null)
                {
                    managerAudioSource.outputAudioMixerGroup = voiceoverMixerGroup;
                }
            }
        }
    }
}