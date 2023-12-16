using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StoryInteractionItem : MonoBehaviour
{
    public GameObject Light;
    public Image StoryPage;
    public TextMeshProUGUI StoryText;
    
    public TextMeshPro TextPrompt;
    private bool playerInTriggerZone = false;
    //private Image StoryPage;
    public float fadeDuration = 1f;
    
    private float targetAlpha = 0f;

    // Start is called before the first frame update
    void Start()
    {   
            // Ensure StoryPage is assigned in the Unity Editor
        TextPrompt.gameObject.SetActive(false);
    }
    
    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            TextPrompt.gameObject.SetActive(true);
            playerInTriggerZone = true;
        }
    }

    void FadeOut() {
        targetAlpha = 0;
    }

    void FadeIn() {
        targetAlpha = 1;
    }

    void SetAlpha(Graphic graphic, float alpha)
    {
        Color color = graphic.color;
        color.a = alpha;
        graphic.color = color;
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
        TextPrompt.gameObject.SetActive(false);
        playerInTriggerZone = false;
        FadeOut();
        }
    }

    void Update()
    {
        Color newPageColor = new Color(StoryPage.color.r, StoryPage.color.g, StoryPage.color.b, 1);
        Color newTextColor = new Color(StoryText.color.r, StoryText.color.g, StoryText.color.b, 1);
        if(Mathf.Abs(StoryPage.color.a - targetAlpha) > 0.01) {
            newPageColor.a = Mathf.Lerp(StoryPage.color.a, targetAlpha, 0.1f);
            newTextColor.a = Mathf.Lerp(StoryText.color.a, targetAlpha, 0.1f);
        
        } else {
            newPageColor.a = targetAlpha;
            newTextColor.a = targetAlpha;
        }
        StoryPage.color = newPageColor;
        StoryText.color = newTextColor;
        

        if (playerInTriggerZone && Input.GetKeyDown(KeyCode.E))
        {   
            Light.SetActive(true);
            TextPrompt.gameObject.SetActive(false);
            FadeIn();
            AudioManager.instance.PageSound();
            
        }
        else if (Input.anyKeyDown)
        {
            GameManager.instance.ResumeGame();
        }
    }
}
