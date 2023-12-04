using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryCameraTriggerController : MonoBehaviour
{
    public Camera mainCamera;
    public Camera secondaryCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
   void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (mainCamera.enabled)
            {
                mainCamera.enabled = false;
                secondaryCamera.enabled = true;
            }
            else
            {
                mainCamera.enabled = true;
                secondaryCamera.enabled = false;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
