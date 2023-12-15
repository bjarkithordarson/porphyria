using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerAudioManager : MonoBehaviour
{
    public static StalkerAudioManager instance;

    public AudioSource audioSource;
    [Header("Despawned")]
    public AudioClip despawnedEnter;
    [Header("Spawning")]
    public AudioClip spawningEnter;
    [Header("Idle")]
    public AudioClip idleEnter;
    [Header("Scared")]
    public AudioClip scaredEnter;
    [Header("Preparing")]
    public AudioClip preparingLungeEnter;
    [Header("Lunging")]
    public AudioClip lungingEnter;

    private void Start()
    {
        if (StalkerAudioManager.instance == null)
        {
            StalkerAudioManager.instance = GetComponent<StalkerAudioManager>() ?? gameObject.AddComponent<StalkerAudioManager>();
        }
    }
    public void PlayDespawnedEnter()
    {
        if(despawnedEnter != null)
        {
            audioSource.clip = despawnedEnter;
            audioSource.Play();
        }
    }

    public void PlaySpawningEnter()
    {
        if (spawningEnter != null)
        {
            audioSource.clip = spawningEnter;
            audioSource.Play();
        }
    }

    public void PlayIdleEnter()
    {
        if(idleEnter != null)
        {
            audioSource.clip = idleEnter;
            audioSource.Play();
        }
    }

    public void PlayScaredEnter()
    {
        if (scaredEnter != null)
        {
            audioSource.clip = scaredEnter;
            audioSource.Play();
        }
    }

    public void PlayPreparingLungeEnter()
    {
        if (preparingLungeEnter != null)
        {
            audioSource.clip = preparingLungeEnter;
            audioSource.Play();
        }
    }

    public void PlayLungingEnter()
    {
        if (lungingEnter != null)
        {
            audioSource.clip = lungingEnter;
            audioSource.Play();
        }
    }

}
