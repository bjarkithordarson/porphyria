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

    public int AmountOfPlacedStatues = 0;
    public int AmountofStatuesNeeded = 4;
    private void Awake()
    {
        instance = this;
    }

    public IEnumerator OutOfOilRoutine()
    {
        //audioSource.PlayOneShot(deathSound);
        yield return new WaitForSeconds(3); // Wait for 3 secs
        SceneManager.LoadScene(deathSceneName); // Load death scene
    }

    public void InstantDeathRoutine()
    {
        SceneManager.LoadScene(deathSceneName); // Restart the game
    }
}