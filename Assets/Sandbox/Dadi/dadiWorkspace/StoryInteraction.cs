using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoryInteraction : MonoBehaviour
{
    public GameObject Light;
    public Canvas StoryCanvas;
    public GameObject Button;
    public TextMeshProUGUI TextPrompt;
    // Start is called before the first frame update
    private bool playerInTriggerZone = false;

    void Start()
    {
        TextPrompt.enabled = false;
        StoryCanvas.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        TextPrompt.enabled = true;
        if (other.gameObject.CompareTag("Player"))
        {
            playerInTriggerZone = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        TextPrompt.enabled = false;
        playerInTriggerZone = false;
        StoryCanvas.enabled = false;
    }

    void Update()
    {
        if (playerInTriggerZone && Input.GetKeyDown(KeyCode.E))
        {
            Light.SetActive(true);
            StoryCanvas.enabled = true;
        }

    else if (Input.anyKeyDown)
    {
        StoryCanvas.enabled = false;
    }
    }

    // void OnTriggerEnter(Collider other)
    // {   
    //     TextPrompt.enabled = true;
    //     if (other.gameObject.CompareTag("Player"))
    //     {
    //         if(Input.GetKeyDown(KeyCode.E))
    //         {
    //             Light.SetActive(true);
    //             StoryCanvas.enabled = true;
    //         }
    //     }
    // }

    // void OnTriggerExit(Collider other)
    // {
    //     TextPrompt.enabled = false;
    // }
    // // Update is called once per frame
    // void Update()
    // {
    //     if(Input.anyKeyDown)
    //     {
    //         StoryCanvas.enabled = false;
    //     }
    // }
}
