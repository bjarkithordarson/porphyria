using UnityEngine;
using UnityEngine.UI;


public class SpotlightController : MonoBehaviour
{
    public float sensitivity = 2f;
    public float maxRotation = 90f; // Maximum rotation in one direction
    public float highIntensity = 10f; // Intensity when the mouse button is held

    private float currentRotation = 0f;
    private float originalIntensity;
    private Light spotlight;
    public LanternController lanternController;
    public Light areaLight;
    public DepletionController depletionController;
    public RawImage fillImage;

    void Start()
    {
        // Get the Light component attached to this GameObject
        spotlight = GetComponent<Light>();
        // Store the original intensity
        originalIntensity = spotlight.intensity;
    }

    void Update()
    {
        // Pause lantern mechanic if game is paused.
        if(GameManager.instance.isPaused)
        {
            return; // Y U NO WORK?
        }
        // Rotate the spotlight based on mouse movement
        float mouseMovement = Input.GetAxis("Mouse X") * sensitivity;
        float newRotation = currentRotation + mouseMovement;
        newRotation = Mathf.Clamp(newRotation, -maxRotation, maxRotation);

        if (newRotation != currentRotation)
        {
            transform.localRotation = Quaternion.Euler(0f, newRotation, 0f);
            currentRotation = newRotation;
        }

        // Adjust intensity based on mouse button
        if (Input.GetMouseButtonDown(0)) // Left mouse button pressed
        {
            spotlight.intensity = highIntensity;
            lanternController.baselineFlickerSpeed = 120;
            lanternController.intenseFlickerSpeed = 120;
            areaLight.range = 20;
            lanternController.canFlicker = false;
            depletionController.HighlightUsage();
            fillImage.color = new Color32(255, 0, 6, 255);
        }
        if (Input.GetMouseButtonUp(0)) // Left mouse button released
        {
            spotlight.intensity = originalIntensity;
            lanternController.baselineFlickerSpeed = 0.1f;
            lanternController.intenseFlickerSpeed = 0.1f;
            lanternController.nextFlickerTime = 0.1f;
            areaLight.range = 8;
            lanternController.canFlicker = true;
            depletionController.HighlightUsageOff();
            fillImage.color = new Color32(255, 234, 97, 255);
        }
    }
}
