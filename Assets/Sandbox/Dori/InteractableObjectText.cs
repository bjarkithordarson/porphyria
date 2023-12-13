using UnityEngine;
using TMPro;

public class InteractableObjectText : MonoBehaviour
{
    public TextMeshPro textPopup; // Assign your TextMeshPro text in the inspector

    void Start()
    {
        // Initially hide the text
        textPopup.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player enters the trigger
        if (other.CompareTag("Player")) // Make sure your player has the tag "Player"
        {
            textPopup.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Hide the text when the player exits the trigger
        if (other.CompareTag("Player"))
        {
            textPopup.gameObject.SetActive(false);
        }
    }
}
