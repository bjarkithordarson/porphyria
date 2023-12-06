using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerController : MonoBehaviour
{
    public GameObject player;
    public float spawnRadius = 1;
    public bool avoidCollisions = true;
    public float collisionSphereRadius = 0f;
    public float spawnAngleFrom = 0;
    public float spawnAngleTo = 2 * Mathf.PI;
    public bool autoSpawnRandomly = false;
    public float respawnRateSeconds = 1f;
    public bool alwaysSpawnInBounds = true; 
    public float yAxisSpawn = 0.2f;
    public AudioSource spawnAudio;
    private System.DateTime lastSpawnTimestamp;
    private double secondsSinceLastSpawn;

    // Start is called before the first frame update
    void Start()
    {
        lastSpawnTimestamp = System.DateTime.Now;
        secondsSinceLastSpawn = 0;
    }

    // Update is called once per frame
    void Update()
    {
        secondsSinceLastSpawn = (System.DateTime.Now - lastSpawnTimestamp).TotalSeconds;
    }

    void SpawnRandomly()
    {
        if(autoSpawnRandomly && (secondsSinceLastSpawn < respawnRateSeconds))
        {
            SpawnInRadius(player.transform.position, spawnRadius);
        }
    }

    void SpawnInRadius(Vector3 center, float radius)
    {
        int retries = 100;
        Vector3 position;
        do
        {
            float angle = (Mathf.Deg2Rad * player.transform.rotation.y) + spawnAngleFrom + (Random.value * (spawnAngleTo));
            position = center + new Vector3(Mathf.Cos(angle), yAxisSpawn, Mathf.Sin(angle)).normalized * radius;
            Debug.Log("Finding random spawn point along circle");
            retries--;

            Debug.Log(IsAboveFloor(position));

            // Prevents the game from hanging. Crashes instead.
            if(retries <= 0)
            {
                throw new System.Exception("Unable to find a collisionless position.");
            }
        } while ((avoidCollisions && IsInsideCollider(position)) || !IsAboveFloor(position));
        Debug.Log("Found a random spawn point!");
        SpawnAt(position);
    }

    private bool IsInsideCollider(Vector3 point)
    {
        Collider[] hitColliders = Physics.OverlapSphere(point, collisionSphereRadius);

        if(hitColliders.Length > 0)
        {
            return true;
        }
        return false;
    }
    private bool IsAboveFloor(Vector3 point)
    {
        return Physics.Raycast(point, Vector3.down, out RaycastHit hit, 5f);
    }
            


    public void SpawnAt(Vector3 position)
    {
        lastSpawnTimestamp = System.DateTime.Now;
        transform.position = position;
        if(spawnAudio && spawnAudio.isPlaying == false)
        {
            spawnAudio.Play();
        }
        transform.LookAt(player.transform);
    }

    void Despawn()
    {

    }
}
