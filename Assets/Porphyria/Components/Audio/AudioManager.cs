using System;
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

    //Oil fucntionality sounds

    public AudioSource FlaskPickupSource;
    public AudioClip FlaskPickupSound;
    public AudioSource OutOfOilSoonSource;
    public AudioClip OutOfOilSoonSound;

    //Sounds for StoryInteractable

    public AudioSource StoryInteractible;
    public AudioClip Page;
    public AudioClip DaughterScript1;
    public AudioClip DaughterScript2;
    public AudioClip DaughterScript3;
    public AudioClip DaughterScript4;

    public AudioClip ScientistScript1;
    public AudioClip ScientistScript2;
    public AudioClip ScientistScript3;
    public AudioClip ScientistScript4;
    public AudioClip DraculasNotes;




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

    public void FlaskPickupBubble()
    {
        PlayOneShot(FlaskPickupSound, FlaskPickupSource);
    }

    public void OutOfOil()
    {
        PlayOneShot(OutOfOilSoonSound, OutOfOilSoonSource);
    }

    public void PageSound()
    {
        PlayOneShot(Page,StoryInteractible);
    }
    public void DaughterScriptSound1()
    {
        PlayOneShot(DaughterScript1, StoryInteractible);
    }
    public void DaughterScriptSound2()
    {
        PlayOneShot(DaughterScript2, StoryInteractible);
    }
    public void DaughterScriptSound3()
    {
        PlayOneShot(DaughterScript3, StoryInteractible);
    }
    public void DaughterScriptSound4()
    {
        PlayOneShot(DaughterScript4, StoryInteractible);
    }
    public void ScientisScriptSound1()
    {
        PlayOneShot(ScientistScript1, StoryInteractible);
    }
    public void ScientisScriptSound2()
    {
        PlayOneShot(ScientistScript2, StoryInteractible);
    }
    public void ScientisScriptSound3()
    {
        PlayOneShot(ScientistScript3, StoryInteractible);
    }
    public void ScientisScriptSound4()
    {
        PlayOneShot(ScientistScript4, StoryInteractible);
    }

}
