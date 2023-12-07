using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public AudioClip deathSound; // Assign this in the inspector
    //private AudioSource audioSource;
    public string deathSceneName = "EndScene";
    public string gameSceneName = "Main_Level";

    IEnumerator OutOfOilRoutine()
    {
        //audioSource.PlayOneShot(deathSound);
        yield return new WaitForSeconds(3); // Wait for 3 secs

        SceneManager.LoadScene(deathSceneName); // Load death scene
        yield return new WaitForSeconds(3); // Wait for 3 secs

        SceneManager.LoadScene(gameSceneName); // Restart the game
    }
}