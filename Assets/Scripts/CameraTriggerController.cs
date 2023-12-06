using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTriggerController : MonoBehaviour
{
    public Camera mainCamera;
    public Camera secondaryCamera;
    // Start is called before the first frame update
    void Start()
    {
        // Ensure only the main camera is active initially
        mainCamera.enabled = true;
        secondaryCamera.enabled = false;
    }
    void OnTriggerEnter(Collider other)
    {
        // Check if the character enters the trigger zone
        if (other.CompareTag("Player")) // Ensure your character has the tag "Player"
        {
            // Switch cameras
            mainCamera.enabled = secondaryCamera;
            secondaryCamera = mainCamera;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}


public class CTrigger2 : MonoBehaviour
{
    public Camera mainCamera;
    public Camera secondaryCamera;

    void Start()
    {
        // Ensure only the main camera is active initially
        mainCamera.enabled = true;
        secondaryCamera.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the character enters the trigger zone
        if (other.CompareTag("Player")) // Ensure your character has the tag "Player"
        {
            // Switch cameras
            mainCamera.enabled = !mainCamera.enabled;
            secondaryCamera.enabled = !secondaryCamera.enabled;
        }
    }
}