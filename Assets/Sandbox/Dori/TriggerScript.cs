using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    public AudioSource soundEffect; 
    public GameObject dustExplosion1;
    public GameObject dustExplosion2; 
    public CameraShakeController cameraShakeController; 
    public Animator fallingRubbleAnimator1;
    public Animator fallingRubbleAnimator2;
    private Collider triggerCollider; 

    private void Start()
    {
        triggerCollider = GetComponent<Collider>(); // Get the collider component
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure it's the player entering the trigger
        {
            soundEffect.Play(); // Play the sound effect
            cameraShakeController.ShakeCamera(1f, 12f); // New camera shake using Cinemachine
            Invoke("ActivateFallingRubble", 6f);
            Invoke("ActivateEffects", 7f); // Wait for 1 second before activating effects
        }
    }

    private void ActivateFallingRubble()
    {
        fallingRubbleAnimator1.enabled = true; // Enable the animation
        fallingRubbleAnimator2.enabled = true; // Enable the animation
    }

    private void ActivateEffects()
    {
        dustExplosion1.SetActive(true);
        dustExplosion2.SetActive(true); // Enable the DustExplosion object
        Invoke("DeactivateEffects", 1.8f); // Schedule to deactivate effects after 3 seconds
    }

    private void DeactivateEffects()
    {
        dustExplosion1.SetActive(false);
        dustExplosion2.SetActive(false); // Disable the DustExplosion object
        triggerCollider.enabled = false; // Disable the trigger collider
    }
}
