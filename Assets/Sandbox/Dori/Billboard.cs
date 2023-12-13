using UnityEngine;
using Cinemachine;
using TMPro;

public class Billboard : MonoBehaviour
{
    public CinemachineBrain cinemachineBrain;



    void Update()
    {
        if (cinemachineBrain != null && cinemachineBrain.ActiveVirtualCamera != null)
        {
            // Get the current active virtual camera
            ICinemachineCamera activeCam = cinemachineBrain.ActiveVirtualCamera;

            // Make the text face the active camera
            transform.LookAt(transform.position + activeCam.State.FinalOrientation * Vector3.forward,
                activeCam.State.FinalOrientation * Vector3.up);
        }
    }
}
