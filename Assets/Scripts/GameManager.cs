using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    //public AudioClip deathSound; // Assign this in the inspector
    //private AudioSource audioSource;
    public static GameManager instance;
    public string deathSceneName = "EndScene";
    public string gameSceneName = "Main_Level";

    private void Awake()
    {
        instance = this;
    }

    public IEnumerator OutOfOilRoutine()
    {
        //audioSource.PlayOneShot(deathSound);
        yield return new WaitForSeconds(3); // Wait for 3 secs
        SceneManager.LoadScene(deathSceneName); // Load death scene

        yield return new WaitForSeconds(5); // Wait for 5 secs
        SceneManager.LoadScene(gameSceneName); // Restart the game
    }
}