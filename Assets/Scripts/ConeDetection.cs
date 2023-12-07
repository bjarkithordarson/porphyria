using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Needed for UI elements
using TMPro;


public class ConeDetection : MonoBehaviour
{

    public Material highlightMaterial; // Assign this in the Inspector
    private Material normalMaterial; // Store the original material
    public TextMeshProUGUI statueCountText; // Change to TextMeshProUGUI
    private int statueCount = 0; // To keep track of statues interacted with
    private GameObject currentStatue = null; // To keep track of the current statue
    public TextMeshProUGUI interactionText;



    void Update()
    {
        // Check if "E" is pressed and there's a current statue
        if (Input.GetKeyDown(KeyCode.E) && currentStatue != null)
        {
            if(statueCount != 2)
            {
            InteractWithStatue();
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Flask"))
        {
            HighlightObject(other.gameObject);
        }
        if (other.gameObject.CompareTag("Statue"))
        {
            HighlightObject(other.gameObject);
            currentStatue = other.gameObject;
            if (statueCount == 2)
            {
                interactionText.text = " You have enough statues ";
            }
            interactionText.enabled = true;
        }
        // Add more conditions here for other interactable objects
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Flask"))
        {
            RemoveHighlight(other.gameObject);
        }
        if (other.gameObject.CompareTag("Statue"))
        {
            RemoveHighlight(other.gameObject);
            currentStatue = null;
            interactionText.text = " Press E to pickup statue ";
            interactionText.enabled = false;
        }
        // Add more conditions here for other interactable objects
    }

    void HighlightObject(GameObject obj)
    {
        var renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            // Store the original material
            if (normalMaterial == null)
            {
                normalMaterial = renderer.material;
            }
            renderer.material = highlightMaterial;
        }
    }

    void RemoveHighlight(GameObject obj)
    {
        var renderer = obj.GetComponent<Renderer>();
        if (renderer != null && normalMaterial != null)
        {
            renderer.material = normalMaterial;
        }
    }

void InteractWithStatue()
{   

        statueCount++; // Increase statue count
        statueCountText.text = statueCount.ToString() + "/2"; // Update TMP text

        Destroy(currentStatue); // Destroy the statue object
        currentStatue = null; // Reset current statue
        interactionText.enabled = false; // Disable interaction text
    }
}

