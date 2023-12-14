using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DirectionalCameraTrigger : MonoBehaviour
{
    public enum Direction
    {
        North=0,
        East=1,
        South=2,
        West=3
    }

    public enum Axis
    {
        NorthSouth,
        EastWest
    }

    [Header("Cameras")]
    public CinemachineVirtualCamera northCamera;
    public CinemachineVirtualCamera southCamera;
    public CinemachineVirtualCamera eastCamera;
    public CinemachineVirtualCamera westCamera;

    [Header("Switching Mechanic")]
    public GameObject trackingObject;
    public Axis axis = Axis.NorthSouth;
    [Range(0, 3600)]
    public float switchDelay = 3f;
    public bool switchWhileMoving = false;

    private Direction currentObjectDirection;
    private DateTime lastObjectDirectionChange;

    private bool isActive = false;
    private bool priorityLowered = false;

    private Vector3 lastObjectPosition;

    private bool isEntering = false;
    private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        currentObjectDirection = GetObjectOrientation();

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Moved: " + ObjectHasMoved());
        //Debug.Log("Last position: " + lastObjectPosition);
        //Debug.Log("Position: " + trackingObject.transform.position);
        isMoving = trackingObject.transform.position.Equals(lastObjectPosition) == false;

        if (currentObjectDirection != GetObjectOrientation())
        {
            currentObjectDirection = GetObjectOrientation();
            lastObjectDirectionChange = DateTime.Now;
        }

        lastObjectPosition = trackingObject.transform.position;
        if (!isActive)
        {
            return;
        }

        /*if((DateTime.Now - lastObjectDirectionChange).TotalSeconds > switchDelay) {
            SwitchCamera(GetObjectOrientation());
        }*/

    }

    private void OnTriggerEnter(Collider other)
    {
        isEntering = true;
        OnTriggerStay(other);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "Player")
        {
            return;
        }
        if (!isEntering && !switchWhileMoving && isMoving)
        {
            Debug.Log("BBBBBBBB" + isMoving + currentObjectDirection);
            return;
        }
        if (!priorityLowered)
        {
            currentObjectDirection = GetObjectOrientation();
            isActive = true;

            CinemachineVirtualCamera[] virtualCameras = GameObject.FindObjectsOfType<CinemachineVirtualCamera>();

            foreach (CinemachineVirtualCamera vcam in virtualCameras)
            {
                vcam.m_Priority = 10;
            }
            priorityLowered = true;
        }

        SwitchCamera(GetObjectOrientation());
        isEntering = false;
    }

    private void OnTriggerExit(Collider other)
    {
        isActive = false;
        priorityLowered = false;
        isEntering = false;
    }

    void SwitchCamera(Direction direction)
    {
        Debug.Log("Switching directions");

        if(northCamera)
            northCamera.m_Priority = direction == Direction.North ? 100 : 10;
        
        if(southCamera)
            southCamera.m_Priority = direction == Direction.South ? 100 : 10;
        
        if(eastCamera)
            eastCamera.m_Priority = direction == Direction.East ? 100 : 10;
        
        if(westCamera)
            westCamera.m_Priority = direction == Direction.West ? 100 : 10;
    }

    public Direction GetObjectOrientation()
    {
        float rotation = trackingObject.transform.eulerAngles.y;

        switch (axis)
        {
            case Axis.NorthSouth:
                if (rotation < 180)
                {
                    return Direction.North;
                }
                else
                {
                    return Direction.South;
                }
            case Axis.EastWest:
                if (rotation < 90 || rotation >= 270)
                {
                    return Direction.East;
                }
                else
                {
                    return Direction.West;
                }
        }
        return Direction.North;
    }
    private void OnDrawGizmos()
    {
        BoxCollider collider = GetComponent<BoxCollider>();
        if (collider == null)
            return;

        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);

        // Draw opaque yellow box
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(collider.center, collider.size);

        // Draw red wireframe
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(collider.center, collider.size);
    }
}
