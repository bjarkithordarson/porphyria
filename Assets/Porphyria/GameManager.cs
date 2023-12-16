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

    public StalkerStateManager stalkerStateMachine;

    public int AmountOfPlacedStatues = 0;
    public int AmountofStatuesNeeded = 4;

    public bool isPaused = false;

    private void Awake()
    {

        instance = this;
      
    }

    private void Update()
    {
        SetStalkerDifficulty();
    }

    private void SetStalkerDifficulty()
    {
        if(AmountOfPlacedStatues == 0)
        {
            stalkerStateMachine.enableSpawn = false;
            stalkerStateMachine.enableLunge = false;
            stalkerStateMachine.despawnedState.spawnTimeout = 30;
            stalkerStateMachine.spawningState.spawnRadius = 10;
            stalkerStateMachine.preparingLungeState.maxLungeDistance = 10;
        }

        if(AmountOfPlacedStatues == 1)
        {
            stalkerStateMachine.enableSpawn = true;
            stalkerStateMachine.enableLunge = false;
            stalkerStateMachine.despawnedState.spawnTimeout = 30;
            stalkerStateMachine.spawningState.spawnRadius = 10;
            stalkerStateMachine.preparingLungeState.maxLungeDistance = 10;
        }

        if(AmountOfPlacedStatues == 2)
        {
            stalkerStateMachine.enableSpawn = true;
            stalkerStateMachine.enableLunge = true;
            stalkerStateMachine.despawnedState.spawnTimeout = 25;
            stalkerStateMachine.spawningState.spawnRadius = 9;
            stalkerStateMachine.preparingLungeState.maxLungeDistance = 10;
        }

        if (AmountOfPlacedStatues == 3)
        {
            stalkerStateMachine.enableSpawn = true;
            stalkerStateMachine.enableLunge = true;
            stalkerStateMachine.despawnedState.spawnTimeout = 15;
            stalkerStateMachine.spawningState.spawnRadius = 6;
            stalkerStateMachine.preparingLungeState.maxLungeDistance = 15;
        }

        if (AmountOfPlacedStatues == 4)
        {
            stalkerStateMachine.enableSpawn = false;
            stalkerStateMachine.enableLunge = false;
            stalkerStateMachine.despawnedState.spawnTimeout = 15;
            stalkerStateMachine.spawningState.spawnRadius = 6;
            stalkerStateMachine.preparingLungeState.maxLungeDistance = 15;
            stalkerStateMachine.TransitionToState(stalkerStateMachine.despawnedState);
        }
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

    public void PauseGame()
    {
        instance.isPaused = true;
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        instance.isPaused = false;
        Time.timeScale = 1f;
    }
}