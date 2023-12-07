using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButtonHandler : MonoBehaviour
{
    public void QuitGame()
    {
        // If we are running in a standalone build of the game
        #if UNITY_STANDALONE
        Application.Quit();
        #endif

        // If we are running in the editor
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}