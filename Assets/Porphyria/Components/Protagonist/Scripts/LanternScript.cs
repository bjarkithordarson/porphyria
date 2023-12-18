using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternScript : MonoBehaviour
{
    public Transform rightArm; // Assign this in the Inspector
    private Quaternion originalRotation;
    private bool isArmRaised = false;

    void Start()
    {
        if (rightArm == null)
        {
            Debug.LogError("Right arm Transform not assigned!");
            return;
        }

        // Store the original rotation of the right arm
        originalRotation = rightArm.localRotation;
    }

    void Update()
    {
        // Check for a specific condition to raise/lower the arm
        // For example, pressing the 'R' key to raise/lower the arm
        if (Input.GetKeyDown(KeyCode.W))
        {
            isArmRaised = !isArmRaised;
        }

        if (isArmRaised)
        {
            // Rotate the right arm to raise it
            rightArm.localRotation = Quaternion.Lerp(rightArm.localRotation, Quaternion.Euler(90, 0, 0), Time.deltaTime * 5);
        }
        else
        {
            // Return the right arm to its original rotation
            rightArm.localRotation = Quaternion.Lerp(rightArm.localRotation, originalRotation, Time.deltaTime * 5);
        }
    }
}
