using UnityEngine;
using System.Collections;

public class LightningTrigger : MonoBehaviour
{
    public Light[] spotlights; // Assign your spotlights in the Inspector
    public AudioSource lightningSound; // Assign an AudioSource for the lightning sound
    public float maxIntensity = 15f; // Maximum intensity of the lightning
    public float flashDuration = 0.2f; // Duration of each flash
    public float intervalBetweenFlashes = 0.1f; // Interval between flashes
    public Animator movingObject;
    private bool hasTriggered = false; // Flag to check if lightning has been triggered

    // OnTriggerEnter is called when another collider enters the trigger collider
    void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player")) // Check if it's the player and if the lightning has not been triggered before
        {
            hasTriggered = true; // Set the flag to true to prevent future triggering
            TriggerLightning();
            Invoke("MoveObject", 2f);
        }
    }

    public void TriggerLightning()
    {
        if (lightningSound != null)
        {
            StartCoroutine(PlayLightningSound()); // Start the coroutine to play the sound with delay
        }

        foreach (Light spotlight in spotlights)
        {
            StartCoroutine(Flash(spotlight));
        }
    }

    private IEnumerator PlayLightningSound()
    {
        yield return new WaitForSeconds(1f); // Wait for 1 second
        lightningSound.Play(); // Play the lightning sound after the delay
    }

    private void MoveObject()
    {
        movingObject.enabled = true;
        AudioManager.instance.prisonJumpScareSound();
    }

    private IEnumerator Flash(Light spotlight)
    {
        float originalIntensity = spotlight.intensity; // Remember original intensity

        // First flash
        spotlight.intensity = maxIntensity; // Set to maximum intensity
        yield return new WaitForSeconds(flashDuration); // Wait for the duration of the flash
        spotlight.intensity = originalIntensity; // Reset to original intensity

        yield return new WaitForSeconds(intervalBetweenFlashes); // Wait before the second flash

        // Second flash
        spotlight.intensity = maxIntensity; // Set to maximum intensity again
        yield return new WaitForSeconds(flashDuration); // Wait for the duration of the second flash
        spotlight.intensity = originalIntensity; // Reset to original intensity again
    }
}
