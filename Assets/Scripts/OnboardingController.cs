using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // 👈 REQUIRED: Gives access to Coroutines (IEnumerator)

[RequireComponent(typeof(AudioSource))]
public class OnboardingController : MonoBehaviour
{
    [Header("📺 Onboarding Screens")]
    public GameObject[] screens;

    [Header("🎙️ Screen Voiceovers")]
    [Tooltip("Match the voiceover audio clips to the screens above (Element 0 = Screen 1 voice, etc.)")]
    public AudioClip[] voiceovers;

    private int currentScreenIndex = 0;
    private AudioSource audioSource;

    // Tracks the active auto-advance timer so we can clear it safely if needed
    private Coroutine autoAdvanceCoroutine;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    void Start()
    {
        if (screens == null || screens.Length == 0)
        {
            Debug.LogError("No screens assigned to the Onboarding Controller!");
            return;
        }

        UpdateScreenVisibility();
    }

    public void GoToNextScreen()
    {
        // Safety: If the user manually clicks a button, stop the timer 
        // so it doesn't accidentally double-advance the screens.
        if (autoAdvanceCoroutine != null)
        {
            StopCoroutine(autoAdvanceCoroutine);
        }

        currentScreenIndex++;

        if (currentScreenIndex >= screens.Length)
        {
            StartGame();
        }
        else
        {
            UpdateScreenVisibility();
        }
    }

    private void UpdateScreenVisibility()
    {
        // 1. Manage screen visibility
        for (int i = 0; i < screens.Length; i++)
        {
            if (screens[i] != null)
            {
                screens[i].SetActive(i == currentScreenIndex);
            }
        }

        // 2. Play voiceover and begin tracking its timeline duration
        PlayCurrentVoiceoverAndTrackTime();
    }

    private void PlayCurrentVoiceoverAndTrackTime()
    {
        if (voiceovers != null && currentScreenIndex < voiceovers.Length && voiceovers[currentScreenIndex] != null)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            AudioClip activeClip = voiceovers[currentScreenIndex];
            audioSource.clip = activeClip;
            audioSource.Play();
            Debug.Log($"🎙️ Playing Onboarding Voiceover: {activeClip.name} ({activeClip.length}s)");

            // 👉 Start the countdown timer based on the active audio clip length
            autoAdvanceCoroutine = StartCoroutine(WaitAndAdvance(activeClip.length));
        }
        else
        {
            // Fallback: If you forgot to assign a voiceover clip, print a warning 
            // but still auto-advance after 4 seconds so the game doesn't get permanently stuck!
            Debug.LogWarning($"No voiceover found for Screen index {currentScreenIndex}. Advancing automatically in 4s.");
            autoAdvanceCoroutine = StartCoroutine(WaitAndAdvance(4.0f));
        }
    }

    // ⏳ The Countdown Timer Engine
    private IEnumerator WaitAndAdvance(float delayInSeconds)
    {
        // Add a small 0.5 second pause after the voice stops speaking for a natural transition
        yield return new WaitForSeconds(delayInSeconds + 0.5f);

        Debug.Log("🔊 Audio Finished! Advancing to the next screen automatically.");
        GoToNextScreen();
    }

    private void StartGame()
    {
        audioSource.Stop();
        Debug.Log("Onboarding Complete! Loading Game...");
        SceneManager.LoadScene("Level 01");
    }
}