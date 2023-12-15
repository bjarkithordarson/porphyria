using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTeleporter : MonoBehaviour
{
    public float maxTeleportDistance = 100.0f;
    public string floorTagName = "Floor";

    public void TeleportRandomly()
    {
        Debug.Log("Teleport!");
        Vector3 randomDirection = Random.insideUnitSphere * maxTeleportDistance;
        randomDirection += transform.position;
        randomDirection.y = 1000;

        RaycastHit hit;
        if (Physics.Raycast(randomDirection, Vector3.down, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.CompareTag(floorTagName))
            {
                transform.position = hit.point;
            }
        }
    }
}
