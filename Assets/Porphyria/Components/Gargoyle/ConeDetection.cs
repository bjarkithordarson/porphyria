
using UnityEngine;
using TMPro;


public class ConeDetection : MonoBehaviour
{
    public static ConeDetection instance;

    public TextMeshProUGUI statueCountText; // Change to TextMeshProUGUI
    public int statueCount = 0; // To keep track of statues interacted with
    public int requiredStatueCount;
    private GameObject currentStatue = null; // To keep track of the current statue
    public TextMeshPro interactionText;

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
        if(statueCount == 0)
        {
            //ResetStatueCount();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Statue"))
        {
            currentStatue = other.gameObject;
            if (statueCount == requiredStatueCount)
            {
                interactionText.text = " You have enough statues ";
            }
            interactionText.gameObject.SetActive(true);
        }
        // Add more conditions here for other interactable objects
    }

    public void ResetStatueCount()
    {
        statueCount = 0;
        interactionText.text = "";
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Statue"))
        {
            currentStatue = null;
            interactionText.text = " Pickup statue ";
            interactionText.gameObject.SetActive(false);
        }
        // Add more conditions here for other interactable objects
    }

void InteractWithStatue()
{   

        statueCount++; // Increase statue count
        statueCountText.text = statueCount.ToString() + "/" + requiredStatueCount; // Update TMP text

        Destroy(currentStatue); // Destroy the statue object
        currentStatue = null; // Reset current statue
        interactionText.gameObject.SetActive(false); // Disable interaction text
    }
}


