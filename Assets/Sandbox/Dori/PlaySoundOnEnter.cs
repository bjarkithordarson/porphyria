using UnityEngine;

public class PlaySoundOnEnter : MonoBehaviour
{
    public AudioSource audioSource; // Assign this in the Inspector
    private Collider triggerCollider; // To reference the collider

    void Start()
    {
        triggerCollider = GetComponent<Collider>(); // Get the collider component
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the one you're interested in
        if (other.CompareTag("Player")) // Ensure the player GameObject has the tag "Player"
        {
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
                triggerCollider.enabled = false; // Disable the collider
            }
        }
    }
}
