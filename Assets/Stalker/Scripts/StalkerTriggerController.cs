using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerTriggerController : MonoBehaviour
{
    public GameObject stalkerSpawner;
    public GameObject stalker;

    public float DespawnTimer;

    private void OnTriggerEnter(Collider other)
    {
        StalkerController stalkerController = stalker.GetComponent<StalkerController>();
        stalkerController.SpawnAt(stalkerSpawner.transform.position);
        stalkerController.DelayedDespawn(DespawnTimer);
    }
}
