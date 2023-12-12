
using UnityEngine;
using TMPro;


public class ConeDetection : MonoBehaviour
{
    public static ConeDetection instance;

    public Material highlightMaterial; // Assign this in the Inspector
    private Material normalMaterial; // Store the original material
    public TextMeshProUGUI statueCountText; // Change to TextMeshProUGUI
    public int statueCount = 0; // To keep track of statues interacted with
    public int requiredStatueCount;
    private GameObject currentStatue = null; // To keep track of the current statue
    public TextMeshProUGUI interactionText;

        void Awake()
    {
        instance = this;
    }


    void Update()
    {
        // Check if "E" is pressed and there's a current statue
        if (Input.GetKeyDown(KeyCode.E) && currentStatue != null)
        {
            if(statueCount != requiredStatueCount)
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
            if (statueCount == requiredStatueCount)
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
        if(statueCount > 0)
        {
        statueCountText.text = statueCount.ToString() + "/" + requiredStatueCount; // Update TMP text
        }
        else
        {
            statueCountText.text = "";
        }

        Destroy(currentStatue); // Destroy the statue object
        currentStatue = null; // Reset current statue
        interactionText.enabled = false; // Disable interaction text
    }
}

