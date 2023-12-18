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
    public bool spawnWithAudio = true;

    private void OnTriggerEnter(Collider other)
    {

        /*if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            StalkerController stalkerController = stalker.GetComponent<StalkerController>();
            stalkerController.SpawnAt(stalkerSpawner.transform.position, spawnWithAudio);
            if (timedDespawn)
            {
                stalkerController.DelayedDespawn(secondsUntilDespawn);
            }
        }*/
    }
}
