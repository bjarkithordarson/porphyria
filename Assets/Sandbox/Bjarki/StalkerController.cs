using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerController : MonoBehaviour
{
    public GameObject player;
    [Header("Spawn Settings")]
    [Range(0f,100f)]
    public float spawnRadius = 1f;
    public bool avoidCollisions = true;

    [Range(0f, 10f)]
    public float collisionSphereRadius = 0f;

    [Range(0f, 360f)]
    public float spawnAngleFrom = 0;

    [Range(0f, 360f)]
    public float spawnAngleTo = 2 * Mathf.PI;
    public bool autoSpawnRandomly = false;
    public float respawnRateSeconds = 1f;
    public bool alwaysSpawnInBounds = true; 
    public float yAxisSpawn = 0.2f;
    public AudioSource spawnAudio;
    private System.DateTime lastSpawnTimestamp;
    private double secondsSinceLastSpawn;
    public bool alwaysLookAtPlayer = true;
    public bool despawnWhenVisible = false;
    public float despawnTimeout = 10f;

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
        if (alwaysLookAtPlayer)
        {
            LookAtPlayer();
        }


        if (autoSpawnRandomly && (secondsSinceLastSpawn > respawnRateSeconds))
        {
            SpawnRandomly();
        }

    }

    private void OnBecameVisible()
    {
        Debug.Log("STALKER VISIBLE");
    }

    private void OnBecameInvisible()
    {
        Debug.Log("STALKER INVISIBLE");
    }

    void LookAtPlayer()
    {
        Vector3 difference = player.transform.position - transform.position;
        difference.y = 0;  // This line ensures the rotation is only around the Y-axis.
        float rotationY = Mathf.Atan2(difference.x, difference.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, rotationY, 0.0f);
    }



    void SpawnRandomly()
    {
        SpawnInRadius(player.transform.position, spawnRadius);
    }

    void SpawnInRadius(Vector3 center, float radius)
    {
        int retries = 100;
        Vector3 position;
        do
        {
            float angle = Mathf.Deg2Rad * (spawnAngleFrom + Random.value * (spawnAngleTo - spawnAngleFrom));
            position = center + new Vector3(Mathf.Cos(angle), yAxisSpawn, Mathf.Sin(angle)).normalized * radius;
            //Debug.Log("Finding random spawn point along circle");
            retries--;

            //Debug.Log(IsAboveFloor(position));

            // Prevents the game from hanging. Crashes instead.
            if(retries <= 0)
            {
                throw new System.Exception("Unable to find a collisionless position.");
            }
            //Debug.Log(IsInsideCollider(position));
        } while ((avoidCollisions && IsInsideCollider(position)) || !IsAboveFloor(position));
        if (!IsInsideCollider(position) && IsAboveFloor(position))
        {
            Debug.Log("Found a random spawn point!");
            SpawnAt(position, true);
        }
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
        if (Physics.Raycast(point, Vector3.down, out RaycastHit hit, 5f))
        {
            if (hit.collider.tag == "Floor")
            {
                return true;
            }
        }
        return false;
    }



    public void SpawnAt(Vector3 position, bool spawnWithAudio)
    {
        gameObject.SetActive(true);
        lastSpawnTimestamp = System.DateTime.Now;
        transform.position = position;
        GroundObject();
        if (spawnWithAudio && spawnAudio && spawnAudio.isPlaying == false)
        {
            spawnAudio.Play();
        }
        transform.LookAt(player.transform);
    }

    void GroundObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
        }
    }

    public void Despawn()
    {
        gameObject.SetActive(false);
    }

    public void DelayedDespawn(float seconds)
    {
        Invoke("Despawn", seconds);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.InstantDeathRoutine();
        }
    }
}
