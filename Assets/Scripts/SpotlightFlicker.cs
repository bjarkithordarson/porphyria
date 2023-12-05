using UnityEngine;

public class SpotlightFlicker : MonoBehaviour
{
    public Light spotlight;
    public Transform targetObject;
    public float intenseFlickerDistance = 5.0f; // Distance for intense flickering

    // Baseline flicker settings
    public float baselineFlickerSpeed = 0.1f;
    public float baselineMinIntensity = 0.5f;
    public float baselineMaxIntensity = 1.0f;

    // Intense flicker settings
    public float intenseFlickerSpeed = 0.8f;
    public float intenseMinIntensity = 0.01f;
    public float intenseMaxIntensity = 3.5f;

    private float nextFlickerTime = 0.0f;

    void Update()
    {
        if (spotlight == null || targetObject == null) return;

        float distance = Vector3.Distance(transform.position, targetObject.position);
        
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
