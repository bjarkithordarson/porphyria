using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StoryInteraction : MonoBehaviour
{
    public GameObject Light;
    public GameObject StoryPage;
    public TextMeshProUGUI StoryText;
    
    public TextMeshPro TextPrompt;
    private bool playerInTriggerZone = false;
    private Image storyImage;
    public float fadeDuration = 1f;
    

    // Start is called before the first frame update
    void Start()
    {   
        storyImage = StoryPage.GetComponentInChildren<Image>();
        TextPrompt.enabled = false;
    }
    
    void OnTriggerEnter(Collider other)
    {
        TextPrompt.enabled = true;
        if (other.gameObject.CompareTag("Player"))
        {
            playerInTriggerZone = true;
        }
    }

        IEnumerator FadeIn()
    {
        // Fading in
        yield return FadeToAlpha(StoryText, 1.0f);
        yield return FadeToAlpha(storyImage, 1.0f);

        // Wait for a moment at fully visible state
        yield return new WaitForSeconds(1.0f);

        // Start the fade out process
        GameManager.instance.PauseGame();
        
    }

    IEnumerator FadeOut()
    {
        // Fading out
        yield return FadeToAlpha(StoryText, 0.0f);
        yield return FadeToAlpha(storyImage, 0.0f);

        // You can repeat the process if needed or perform other actions
    }
        IEnumerator FadeToAlpha(Graphic graphic, float targetAlpha)
    {
        Color startColor = graphic.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);

        float elapsedTime = 0.0f;
        while (elapsedTime < fadeDuration)
        {
            graphic.color = Color.Lerp(startColor, targetColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        graphic.color = targetColor;  // Ensure the target alpha is reached exactly
    }

    void SetAlpha(Graphic graphic, float alpha)
    {
        Color color = graphic.color;
        color.a = alpha;
        graphic.color = color;
    }
    void OnTriggerExit(Collider other)
    {
        TextPrompt.enabled = false;
        playerInTriggerZone = false;
        StartCoroutine(FadeOut());
    }

    void Update()
    {   
        if (playerInTriggerZone && Input.GetKeyDown(KeyCode.E))
        {   
            Light.SetActive(true);
            TextPrompt.enabled =false;
            StartCoroutine(FadeIn());
            AudioManager.instance.PageSound();
            
        }

    else if (Input.anyKeyDown)
    {
        StartCoroutine(FadeOut());
        GameManager.instance.ResumeGame();
    }
    }
}
