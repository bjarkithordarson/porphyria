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
    public string tutorialSceneName = "Tutorial";
    public string gameSceneName = "MainLevelScene";

    public int AmountOfPlacedStatues = 0;
    public int AmountofStatuesNeeded = 4;

    public bool isPaused = false;
    private void Awake()
    {
        instance = this;
    }



    public IEnumerator OutOfOilRoutine()
    {
        //audioSource.PlayOneShot(deathSound);
        yield return new WaitForSeconds(4000); // Wait for 3 secs
        SceneManager.LoadScene(deathSceneName); // Load death scene
    }

    public void InstantDeathRoutine()
    {
        SceneManager.LoadScene(deathSceneName); // Restart the game
    }

    public void PauseGame ()
    {
        isPaused = true;
        Time.timeScale = 0f;
    }
    public void ResumeGame ()
    {
        isPaused = false;
        Time.timeScale = 1f;
    }
}