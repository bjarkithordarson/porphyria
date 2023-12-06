using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeDetection : MonoBehaviour
{

    public Material highlightMaterial; // Assign this in the Inspector
    private Material normalMaterial; // Store the original material


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Flask"))
        {
            HighlightObject(other.gameObject);
        }
        if (other.gameObject.CompareTag("Statue"))
        {
            HighlightObject(other.gameObject);
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
}
