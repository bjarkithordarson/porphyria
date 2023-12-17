using System.Collections; // Needed for IEnumerator
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class CreditsRoll : MonoBehaviour
{
    public float targetSpeed = 20f;
    public float delayBeforeStart = 5f;
    public float accelerationDuration = 3f; // Duration over which speed increases
    private float currentSpeed = 0f;
    private RectTransform rectTransform;

    void Start()
    {
        StartCoroutine(LoadNewSceneAfterDelay());
        rectTransform = GetComponent<RectTransform>();
        StartCoroutine(BeginAfterDelay());
    }

    IEnumerator BeginAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeStart);
        StartCoroutine(AccelerateText());
    }

    IEnumerator AccelerateText()
    {
        float elapsedTime = 0f;
        while (elapsedTime < accelerationDuration)
        {
            elapsedTime += Time.deltaTime;
            currentSpeed = Mathf.Lerp(0, targetSpeed, elapsedTime / accelerationDuration);
            rectTransform.position += Vector3.up * currentSpeed * Time.deltaTime;
            yield return null;
        }
        StartCoroutine(MoveText());
    }

    IEnumerator MoveText()
    {
        // Continues moving at target speed
        while (true)
        {
            rectTransform.position += Vector3.up * targetSpeed * Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator LoadNewSceneAfterDelay()
    {
        yield return new WaitForSeconds(66f); // Wait for 66 seconds
        SceneManager.LoadScene("AlphaMenu"); // Replace with your scene name
    }
}