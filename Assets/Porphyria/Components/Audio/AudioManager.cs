using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class AudioManager : MonoBehaviour
{   
    public static AudioManager instance;

    //Sounds for statue placement
    public AudioClip StatuePlacement;
    public AudioSource StatueSource;

    //Sounds for Stone Hatch movement

    public AudioClip StoneSlabPlacement;
    public AudioSource StoneSlabSource;
    public AudioSource FireFloorExplosionSource;
    public AudioClip FireFloorExpSound;


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
        PlayOneShot(StatuePlacement, StatueSource);
    }

    public void StoneHatchSound()
    {
        PlayOneShot(StoneSlabPlacement, StoneSlabSource);
    }
    public void FireFloorExplosion()
    {
        PlayOneShot(FireFloorExpSound, FireFloorExplosionSource);
    }

}
