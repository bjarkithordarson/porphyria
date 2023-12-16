using UnityEngine;
using TMPro;

public class StatueScript : MonoBehaviour
{
    public TextMeshPro interactionText;
    public ConeDetection coneDetection; // Reference to the ConeDetection script

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Check if the player has reached the required statue count
            if (coneDetection.statueCount == coneDetection.requiredStatueCount)
            {
                interactionText.text = "You can only carry 1 statue";
            }
            else
            {
                interactionText.text = "Pick up statue (E)";
            }

            // Enable the interaction text
            interactionText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Disable text when the player leaves
            interactionText.gameObject.SetActive(false);
        }
    }

    public void Interact()
    {
        // Add any specific interaction logic here, like updating counters
        // Disable text upon interaction
        interactionText.gameObject.SetActive(false);
    }
}
