using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerAudioManager : MonoBehaviour
{
    public static StalkerAudioManager instance;

    public AudioSource audioSource;
    public AudioSource ambianceAudioSource;
    [Header("General")]
    public AudioClip ambiance;
    [Range(0,1)]
    public float ambianceVolume = 1;
    public AudioClip failedSpawn;
    [Range(0, 1)]
    public float failedSpawnVolume = 1;
    [Header("Despawned")]
    public AudioClip despawnedEnter;
    [Range(0, 1)]
    public float despawnedEnterVolume = 1;
    [Header("Spawning")]
    public AudioClip spawningEnter;
    [Range(0, 1)]
    public float spawningEnterVolume = 1;
    [Header("Idle")]
    public AudioClip idleEnter;
    [Range(0, 1)]
    public float idleEnterVolume = 1;
    public AudioClip firstSeenByCamera;
    [Range(0,1)]
    public float firstSeenByCameraVolume = 1;
    [Header("Scared")]
    public AudioClip scaredEnter;
    [Range(0, 1)]
    public float scaredEnterVolume = 1;
    [Header("Preparing")]
    public AudioClip preparingLungeEnter;
    [Range(0, 1)]
    public float preparingLungeEnterVolume = 1;
    [Header("Lunging")]
    public AudioClip lungingEnter;
    [Range(0, 1)]
    public float lungingEnterVolume = 1;

    private void Start()
    {
        audioSource.enabled = true;
        ambianceAudioSource.enabled = true;
        if (StalkerAudioManager.instance == null)
        {
            StalkerAudioManager.instance = GetComponent<StalkerAudioManager>() ?? gameObject.AddComponent<StalkerAudioManager>();
        }
    }

    public void Mute()
    {
        audioSource.volume = 0;
        ambianceAudioSource.volume = 0;
    }

    public void Unmute()
    {
        ambianceAudioSource.volume = ambianceVolume;
    }

    public void PlayAmbiance()
    {
        if (ambiance != null)
        {
            ambianceAudioSource.volume = ambianceVolume;
            ambianceAudioSource.clip = ambiance;
            ambianceAudioSource.Play();
        }
    }

    public void StopAmbiance()
    {
        ambianceAudioSource.Stop();
    }

    public void PlayFailedSpawn()
    {
        if (failedSpawn != null)
        {
            audioSource.volume = failedSpawnVolume;
            audioSource.clip = failedSpawn;
            audioSource.Play();
        }
    }


    public void PlayDespawnedEnter()
    {
        if (despawnedEnter != null)
        {
            audioSource.volume = despawnedEnterVolume;
            audioSource.clip = despawnedEnter;
            audioSource.Play();
        }
    }

    public void PlaySpawningEnter()
    {
        if (spawningEnter != null)
        {
            audioSource.volume = spawningEnterVolume;
            audioSource.clip = spawningEnter;
            audioSource.Play();
        }
    }

    public void PlayFirstSeenByCamera()
    {
        if (firstSeenByCamera!= null)
        {
            audioSource.volume = firstSeenByCameraVolume;
            audioSource.clip = firstSeenByCamera;
            audioSource.Play();
        }
    }

    public void PlayIdleEnter()
    {
        if(idleEnter != null)
        {
            audioSource.volume = idleEnterVolume;
            audioSource.clip = idleEnter;
            audioSource.Play();
        }
    }

    public void PlayScaredEnter()
    {
        if (scaredEnter != null)
        {
            audioSource.volume = scaredEnterVolume;
            audioSource.clip = scaredEnter;
            audioSource.Play();
        }
    }

    public void PlayPreparingLungeEnter(float volume = 1)
    {
        if (preparingLungeEnter != null)
        {
            audioSource.volume = preparingLungeEnterVolume;
            audioSource.clip = preparingLungeEnter;
            audioSource.Play();
        }
    }

    public void PlayLungingEnter(float volume = 1)
    {
        if (lungingEnter != null)
        {
            audioSource.volume = lungingEnterVolume;
            audioSource.clip = lungingEnter;
            audioSource.Play();
        }
    }

}
