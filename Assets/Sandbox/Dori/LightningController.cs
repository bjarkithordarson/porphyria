using UnityEngine;
using System.Collections;

public class LightningController : MonoBehaviour
{
    public Light[] spotlights; // Assign your spotlights in the Inspector
    public AudioSource lightningSound; // Assign an AudioSource for the lightning sound
    public float maxIntensity = 15f; // Maximum intensity of the lightning
    public float flashDuration = 0.2f; // Duration of each flash
    public float intervalBetweenFlashes = 0.1f; // Interval between flashes

    void Start()
    {
        // Schedule the lightning to start after 27 seconds and repeat every 30 seconds
        InvokeRepeating("TriggerLightning", 15f, 30f);
    }

    public void TriggerLightning()
    {
        if (lightningSound != null)
        {
            lightningSound.Play(); // Play the lightning sound
        }

        foreach (Light spotlight in spotlights)
        {
            StartCoroutine(Flash(spotlight));
        }
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
