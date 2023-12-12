using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class AudioManager : MonoBehaviour
{   

    public AudioClip StatuePlacement;
    public AudioSource StatueSource;

    public static AudioManager instance;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlayOneShot(AudioClip clip, AudioSource source)
    {
        if (clip != null && source != null)
        {
            source.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("Clip not found");
        }
    
    }

    public void StatuePlacementSound()
    {
        PlayOneShot(StatuePlacement,StatueSource);
    }

}
