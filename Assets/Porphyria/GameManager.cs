using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

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

    [Header("UI")]
    public UIDocument pauseMenu;

    [Header("Settings")]
    [Range(0, 1)]
    public float masterVolume = 0.7f;
    [Range(0, 2)]
    public float brightness= 0.5f;
    public float minBrightness = 0.05f;
    public AudioListener audioListener;

    public PostProcessProfile brightnessProfile;

    AutoExposure exposure;

    private void Awake()
    {
        instance = this;

        brightnessProfile.TryGetSettings(out exposure);

        Debug.Log(brightnessProfile);
    }

    private void Update()
    {
        if(stalkerStateMachine)
        {
            SetStalkerDifficulty();
        }

        UpdateVolume(masterVolume);
        UpdateBrightness(brightness);
    }

    private void UpdateVolume(float volume)
    {
        AudioListener.volume = volume;
    }
    private void UpdateBrightness(float value)
    {
        exposure.keyValue.value = value < minBrightness ? minBrightness : value;
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
            stalkerStateMachine.enableLunge = true;
            stalkerStateMachine.despawnedState.spawnTimeout = 20;
            stalkerStateMachine.spawningState.spawnRadius = 10;
            stalkerStateMachine.preparingLungeState.maxLungeDistance = 5;
        }

        if(AmountOfPlacedStatues == 2)
        {
            stalkerStateMachine.enableSpawn = true;
            stalkerStateMachine.enableLunge = true;
            stalkerStateMachine.despawnedState.spawnTimeout = 15;
            stalkerStateMachine.spawningState.spawnRadius = 9;
            stalkerStateMachine.preparingLungeState.maxLungeDistance = 10;
        }

        if (AmountOfPlacedStatues == 3)
        {
            stalkerStateMachine.enableSpawn = true;
            stalkerStateMachine.enableLunge = true;
            stalkerStateMachine.despawnedState.spawnTimeout = 10;
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

    public void PauseGame(bool audio=true, bool time=true)
    {
        instance.isPaused = true;
        
        if(time)
            Time.timeScale = 0f;
        
        if(audio)
            AudioListener.pause = true;
    }
    public void ResumeGame()
    {
        instance.isPaused = false;
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }

    public void OpenPauseMenu()
    {
        if (pauseMenu != null)
        {
            pauseMenu.enabled = true;
        }
    }
    public void ClosePauseMenu()
    {
        if (pauseMenu != null)
        {
            pauseMenu.enabled = false;
        }
    }
}