using JetBrains.Annotations;
using UnityEngine;

public class DepletionController : MonoBehaviour
{
    public MagicPigGames.ProgressBar progressBar; // Assign this in the inspector
    public Transform player; // Assign this in the inspector
    public Transform targetObject; // Assign this in the inspector
    public float maxDistance = 10.0f; // The maximum distance for full speed depletion
    public Light spotlight;

    private float countdownTimer = 20.0f; // 20 seconds duration
    private float maxTimer = 120.0f; // Maximum value of the timer
    public void SetCountdownTimer()
    {
        countdownTimer = maxTimer;
    }
    private void Update()
    {
        // Base depletion rate
        float baseDepletionRate = 0.2f; // Adjust this value as needed

        // Adjust additional depletion rate based on distance to the object
        float distance = Vector3.Distance(player.position, targetObject.position);
        distance = Mathf.Clamp(distance, 0, maxDistance);

        // Calculate additional depletion rate based on proximity
        float proximityDepletionRate = (1 - (distance / maxDistance)) * baseDepletionRate;

        // Total depletion rate
        float totalDepletionRate = baseDepletionRate + proximityDepletionRate;

        // Decrease the timer and update the progress
        if (countdownTimer > 0)
        {
            countdownTimer -= Time.deltaTime * totalDepletionRate;
            float newProgress = countdownTimer / maxTimer; // Map the timer value to a 0-1 range
            progressBar.SetProgress(newProgress);
        }
        else
        {
            // Ensure the progress doesn't go below zero
            progressBar.SetProgress(0f);
            // Disable the spotlight when progress reaches 0
            if (spotlight != null)
            {
                spotlight.enabled = false;
                StartCoroutine(GameManager.instance.OutOfOilRoutine());
            }
        }
    }
}