using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameController : MonoBehaviour
{
    public AudioSource draculaClip;
    public bool endStoryStarted = false;


    // Update is called once per frame
    void Update()
    {
        if(draculaClip.isPlaying && !endStoryStarted)
        {
            endStoryStarted = true;
            Invoke("EndGame", draculaClip.clip.length + 2f);
        
        }
        
        
    }

    public void EndGame()
    {
        SceneManager.LoadScene("AlphaMenu");
    }
}
