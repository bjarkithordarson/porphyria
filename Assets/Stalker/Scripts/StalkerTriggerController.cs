using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerTriggerController : MonoBehaviour
{
    public GameObject stalkerSpawner;
    public GameObject stalker;
    public bool timedDespawn;
    public float secondsUntilDespawn;

    public float DespawnTimer;

    private void OnTriggerEnter(Collider other)
    {
<<<<<<< HEAD
        StalkerController stalkerController = stalker.GetComponent<StalkerController>();
        stalkerController.SpawnAt(stalkerSpawner.transform.position);
        stalkerController.DelayedDespawn(DespawnTimer);
=======

        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            StalkerController stalkerController = stalker.GetComponent<StalkerController>();
            stalkerController.SpawnAt(stalkerSpawner.transform.position);
            if (timedDespawn)
            {
                stalkerController.DelayedDespawn(secondsUntilDespawn);
            }
        }
>>>>>>> 4972dcc18d180cc9ea04be81cd817f6b873943ac
    }
}
