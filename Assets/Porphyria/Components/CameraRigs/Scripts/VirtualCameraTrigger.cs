using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCameraTrigger : MonoBehaviour
{
    public CinemachineVirtualCamera activeVirtualCamera;
    private CinemachineVirtualCamera[] virtualCameras;
    // Start is called before the first frame update
    void Start()
    {
        virtualCameras = GameObject.FindObjectsOfType<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerStay(other);
    }

    private void OnTriggerStay(Collider other)
    {
        activeVirtualCamera.m_Priority = 100;

        foreach(CinemachineVirtualCamera vcam in virtualCameras)
        {
            if(vcam == activeVirtualCamera)
            {
                continue;
            }
            vcam.m_Priority = 10;
        }

        Debug.Log("VCam trigger");
    }

    private void OnDrawGizmos()
    {
        BoxCollider collider = GetComponent<BoxCollider>();
        if (collider == null)
            return;

        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);

        // Draw opaque yellow box
        Gizmos.color = new Color(1, 1, 0, 0.5f);
        Gizmos.DrawCube(collider.center, collider.size);

        // Draw red wireframe
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(collider.center, collider.size);
    }
}
