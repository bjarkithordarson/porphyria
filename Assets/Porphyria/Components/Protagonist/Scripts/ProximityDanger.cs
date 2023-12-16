using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ProximityDanger : MonoBehaviour
{
    // Start is called before the first frame update

    public float intensity = 0;
        public float heartbeatSpeed = 0.05f; // Adjust the heartbeat speed

    PostProcessVolume _volume;
    Vignette _vignette;
    void Start()
    {
       _volume = GetComponent<PostProcessVolume>(); 

       _volume.profile.TryGetSettings<Vignette>(out _vignette);

       if(!_vignette)
       {
            print("error, vignette empty");
       }
       else
       {
            _vignette.enabled.Override(false);
       }
    }

    // public void PlayerSafe(){
    //     float startingIntensity = 0.6f;
    //     float endIntensity = 0.0f;
    // while (startingIntensity > endIntensity)
    // {
    //     startingIntensity -= 0.05f;
        
        

    //     if (startingIntensity < endIntensity) startingIntensity = endIntensity;

    //     _vignette.intensity.Override(startingIntensity);

    //     // Adjust the duration between each iteration to match the regular heartbeat
    //     _vignette.enabled.Override(false);
    // }

    // }
    // public void PlayerDangerEffect()
    // {

    // float startingIntensity = 0.6f;
    // _vignette.enabled.Override(true);
    // _vignette.intensity.Override(startingIntensity);
    // }

   public void PlayerSafe()
    {
        StartCoroutine(FadeOutVignette());
    }

    IEnumerator FadeOutVignette()
    {
        while (_vignette.intensity > 0)
        {
            _vignette.intensity.Override(_vignette.intensity - heartbeatSpeed);
            yield return null;
        }
        _vignette.enabled.Override(false);
    }

    public void PlayerDangerEffect()
    {
        StartCoroutine(PulseVignette());
    }

    IEnumerator PulseVignette()
    {
        _vignette.enabled.Override(true);

        while (_vignette.intensity < intensity)
        {
            _vignette.intensity.Override(_vignette.intensity + heartbeatSpeed);
            yield return null;
        }

        // Hold the intensity at the maximum for continuous heartbeat simulation
        while (true)
        {
            yield return null;
        }
    }

}
