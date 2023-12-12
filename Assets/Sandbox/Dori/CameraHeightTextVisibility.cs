using UnityEngine;
using TMPro; // Make sure to include the TextMeshPro namespace

public class CameraHeightTextVisibility : MonoBehaviour
{
    public GameObject textMeshProObject; // Assign your TextMeshPro UI GameObject here
    public Camera mainCamera; // Assign your main camera here

    private float visibilityThreshold = 4.9361f; // The Y value threshold

    void Update()
    {
        if (mainCamera.transform.position.y < visibilityThreshold)
        {
            // If the camera's Y position is lower than the threshold, disable the text
            textMeshProObject.SetActive(false);
        }
        else
        {
            // If the camera's Y position is higher than or equal to the threshold, enable the text
            textMeshProObject.SetActive(true);
        }
    }
}
