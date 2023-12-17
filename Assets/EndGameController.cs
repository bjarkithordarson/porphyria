using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class EndGameController : MonoBehaviour
{
    public AudioSource draculaClip;
    public bool endStoryStarted = false;
    public GameObject camera1;
    public GameObject camera2;
    public GameObject paper;
    public GameObject papertext;
    public GameObject mindy;
    public Light spotLight;
    public AudioSource papa;
    public GameObject stopper;





    // Update is called once per frame
    void Update()
    {
        if(draculaClip.isPlaying && !endStoryStarted)
        {
            
            endStoryStarted = true;
            stopper.SetActive(true);
            Invoke("SwitchToFinalCamera", draculaClip.clip.length);
            Invoke("EndGame", draculaClip.clip.length + 3f);
        
        }
        
        
    }

    public void EndGame()
    {
        SceneManager.LoadScene("AlphaMenu");
    }

    public void SwitchToFinalCamera() {
        paper.SetActive(false);
        papertext.SetActive(false);
        camera1.SetActive(false);
        camera2.SetActive(true);
        mindy.SetActive(true);
        spotLight.intensity = 10.0f;
        Invoke("Papa", 1.5f);
    }

    public void Papa()
    {
        papa.enabled = true;
    }
}
