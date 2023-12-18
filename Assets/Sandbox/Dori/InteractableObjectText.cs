using UnityEngine;
using TMPro;

public class InteractableObjectText : MonoBehaviour
{
    public TextMeshPro textPopup; // Assign your TextMeshPro text in the inspector

    // Flag to determine if the player is within the trigger area
    private bool isPlayerInTrigger = false;
    public DepletionController depletionController;
    public MagicPigGames.ProgressBar progressBar;
    
    

    void Update()
    {
        // Check for interaction key press
        if (Input.GetKeyDown(KeyCode.E) && isPlayerInTrigger)
        {
            // Call the function to handle flask consumption
            ConsumeFlask();
        }
    }

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
            isPlayerInTrigger = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Hide the text when the player exits the trigger
        if (other.CompareTag("Player"))
        {
            textPopup.gameObject.SetActive(false);
            isPlayerInTrigger = false;
        }
    }

    void ConsumeFlask()
    {
        depletionController.SetCountdownTimer();
        progressBar.SetProgress(1f);
        AudioManager.instance.FlaskPickupBubble();

        // Destroy the flask object
        Destroy(gameObject); // This will destroy the GameObject this script is attached to
    }
}
