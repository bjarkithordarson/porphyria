using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public static CameraHandler instance;

    public GameObject[] cameras;
    private GameObject mainCamera;

    // public GameObject triggeredCamera { get{return mainCamera;} set{mainCamera = value;} }

    public ThirdPersonController Character;
    // Start is called before the first frame update

    void Awake()
    {
        instance = this;
    }

    void Start() 
    {
        SetActiveCamera(cameras[0]);
    }

    public void SetActiveCamera(GameObject triggeredCamera)
    {
        foreach(var cam in cameras){
            cam.SetActive(false);
        }
        mainCamera = triggeredCamera;
        mainCamera.SetActive(true);
        Character.PlayerCamera = mainCamera;
        //Debug.Log($"Set Active Camera {triggeredCamera.name}");
    }

    // Update is called once per frame
    void Update()
    {    

    }

}
