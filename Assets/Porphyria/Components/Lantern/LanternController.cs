using MagicPigGames;
using UnityEngine;
using UnityEngine.UI; // For UI elements like progress bar
using TMPro;

public class LanternController : MonoBehaviour
{
    
    public MagicPigGames.ProgressBar progressBar; // Assign this in the Inspector
    public Light spotlight;
    public Transform targetObject;
    public DepletionController depletionController;
    public float intenseFlickerDistance = 10.0f; // Distance for intense flickering

    // Baseline flicker settings
    public float baselineFlickerSpeed = 0.1f;
    public float baselineMinIntensity = 0.5f;
    public float baselineMaxIntensity = 1.0f;

    // Intense flicker settings
    public float intenseFlickerSpeed = 0.8f;
    public float intenseMinIntensity = 0.01f;
    public float intenseMaxIntensity = 3.5f;

    //private float nextFlickerTime = 0.0f;
    public float nextFlickerTime = 0.0f;

    public bool canFlicker = true;

     public TextMeshProUGUI interactionText;

    void Update()
    {


        if (spotlight == null || targetObject == null) return;

        
        float distance = Vector3.Distance(transform.position, targetObject.position);
        
        if (canFlicker && targetObject.GetComponent<StalkerController>().isSpawned)
        {
        // Determine if we are doing intense flickering or baseline flickering
        bool isIntenseFlickering = distance < intenseFlickerDistance;

        // Choose flicker parameters based on proximity
        float currentFlickerSpeed = isIntenseFlickering ? intenseFlickerSpeed : baselineFlickerSpeed;
        float minIntensity = isIntenseFlickering ? intenseMinIntensity : baselineMinIntensity;
        float maxIntensity = isIntenseFlickering ? intenseMaxIntensity : baselineMaxIntensity;

        if (Time.time >= nextFlickerTime)
        {
            spotlight.intensity = Random.Range(minIntensity, maxIntensity);
            nextFlickerTime = Time.time + currentFlickerSpeed;
        }
    }
    }


    // Add more methods here for other interactions, like picking up a lantern
}
