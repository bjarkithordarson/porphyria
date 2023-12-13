using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private Direction currentObjectDirection;
    private DateTime lastObjectDirectionChange;

    private bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        currentObjectDirection = GetObjectOrientation();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentObjectDirection != GetObjectOrientation())
        {
            currentObjectDirection = GetObjectOrientation();
            lastObjectDirectionChange = DateTime.Now;
        }

        if (!isActive)
        {
            return;
        }

        

        Debug.Log((DateTime.Now - lastObjectDirectionChange).TotalSeconds > switchDelay);

        if((DateTime.Now - lastObjectDirectionChange).TotalSeconds > switchDelay) {
            SwitchCamera(GetObjectOrientation());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        currentObjectDirection = GetObjectOrientation();
        isActive = true;

        CinemachineVirtualCamera[] virtualCameras = GameObject.FindObjectsOfType<CinemachineVirtualCamera>();

        foreach (CinemachineVirtualCamera vcam in virtualCameras)
        {
            vcam.m_Priority = 10;
        }

        SwitchCamera(GetObjectOrientation());
    }

    private void OnTriggerExit(Collider other)
    {
        isActive = false;
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

        Debug.Log("North: " + northCamera.m_Priority);
        Debug.Log("South: " + southCamera.m_Priority);
        Debug.Log("East: " + eastCamera.m_Priority);
        Debug.Log("West: " + westCamera.m_Priority);
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
}
