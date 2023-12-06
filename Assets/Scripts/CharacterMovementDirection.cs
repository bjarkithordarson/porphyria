using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class CharaterMovementDirection : MonoBehaviour
{
    public Camera[] newCamera;
    private int currentCameraIndex;
    void Start(){
        currentCameraIndex = 0;
        newCamera[currentCameraIndex].enabled = true;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C)){
            newCamera[currentCameraIndex].enabled = false;
            currentCameraIndex++;
            if (currentCameraIndex  >= newCamera.Length)
            {
                currentCameraIndex = 0;
            }
            newCamera[currentCameraIndex].enabled = true;
        }
        
    }
}