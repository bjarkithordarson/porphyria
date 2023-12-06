using Cinemachine;
using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharaterMovementDirection : MonoBehaviour
{
    public Camera[] newCamera;
    public GameObject[] cameralist;
    private int currentCameraIndex;
    public ThirdPersonController Character;
        
    
    
    void Start(){
        currentCameraIndex = 0;
        newCamera[currentCameraIndex].enabled = true;
        Character.PlayerCamera = cameralist[currentCameraIndex];

    }
    void Update()
    {
        Character.PlayerCamera= cameralist[currentCameraIndex];
        if(Input.GetKeyDown(KeyCode.C)){
            newCamera[currentCameraIndex].enabled = false;
            currentCameraIndex++;
            cameralist[currentCameraIndex].SetActive(false);
            if (currentCameraIndex  >= newCamera.Length)
            {
                currentCameraIndex = 0;
            }
            newCamera[currentCameraIndex].enabled = true;
        }
        
    }
}