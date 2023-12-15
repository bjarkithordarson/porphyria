using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerController : MonoBehaviour
{
    public CapsuleCollider stalkerCollider;
    public GameObject stalkerBody;

    //public Animator animator;

    public float lungeSpeed = 2;

    public Vector3 destination;
    void Start()
    {
        destination = transform.position;
        //animator = stalkerBody.GetComponent<Animator>();

        if (stalkerCollider == null)
        {
            throw new System.Exception("Stalker needs a capsule collider. Add a collider to the stalker.");
        }

        if (stalkerBody == null)
        {
            throw new System.Exception("Stalker needs a model. Add a model to the stalker.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Vector3.Distance(transform.position, destination) > 0.01f)
            transform.position = Vector3.Lerp(transform.position, destination, 0.01f);
        else
            transform.position = destination;
    }

    public void Spawn()
    {
        stalkerBody.SetActive(true);
        StalkerAudioManager.instance.PlayAmbiance();
    }
    public void Despawn()
    {
        stalkerBody.SetActive(false);
        StalkerAudioManager.instance.StopAmbiance();
    }
    public void TeleportTo(Vector3 position)
    {
        transform.position = position;
        destination = position;
    }
    public void LookAt(Vector3 position)
    {
        Vector3 direction = (position - transform.position).normalized;
        direction.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        transform.rotation = lookRotation;
    }
    public bool CanSee(Vector3 position)
    {
        Vector3 origin = transform.position;
        origin.y += 1;
        RaycastHit hit;
        Vector3 direction = position - transform.position;

        //Debug.DrawRay(origin, direction);

        if (Physics.Raycast(origin, direction, out hit))
        {
            return hit.collider.CompareTag("Player");
        }

        return false;
    }

    public void StartIdleAnimation()
    {
        GetComponent<Animator>().Play("Idle");
    }
    public void StartFloatingAnimation()
    {
        GetComponent<Animator>().Play("Floating");
    }

    public void RunTo(Vector3 position)
    {
        destination = position;
    }

    public bool VisibleByCamera()
    {
        return stalkerBody.GetComponent<MeshRenderer>().isVisible;
    }

    public void TeleportRandomly()
    {
        TeleportTo(GetRandomPositionOnFloor());
    }

    public void TeleportRandomlyInRadius(Vector3 position, float radius, float minAngleDegrees, float maxAngleDegrees, float rotation)
    {
        TeleportTo(GetRandomPositionInRadius(position, radius, minAngleDegrees, maxAngleDegrees, rotation));
    }

    public Vector3 GetRandomPositionOnFloor()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 10f;
        randomDirection += transform.position;
        randomDirection.y = 1000;

        RaycastHit hit;
        if (Physics.Raycast(randomDirection, Vector3.down, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.CompareTag("Floor"))
            {
                return hit.point;
            }
        }
        return Vector3.zero;
    }
    public Vector3 GetRandomPositionInRadius(Vector3 position, float radius, float minAngleDegrees, float maxAngleDegrees, float rotation)
    {
        // Convert angles in degrees to radians
        float minAngle = (minAngleDegrees + rotation) * Mathf.Deg2Rad;
        float maxAngle = (maxAngleDegrees + rotation) * Mathf.Deg2Rad;

        // Generate random angle between minAngle and maxAngle
        float angle = Random.Range(minAngle, maxAngle);

        // Calculate new x and z positions
        float x = position.x + radius * Mathf.Cos(angle);
        float z = position.z + radius * Mathf.Sin(angle);

        // Create a new Vector3 with the calculated x and z
        Vector3 newPos = new Vector3(x, position.y + 0.1f, z);

        // Cast a ray downwards from the new position
        RaycastHit hit;
        if (Physics.Raycast(newPos, Vector3.down, out hit))
        {
            // If the ray hits an object tagged as "Floor", adjust the y position
            if (hit.collider.tag == "Floor")
            {
                newPos.y = hit.point.y;
            }
        }

        // Return new Vector3 position
        return newPos;
    }

    public bool IsCapsuleColliding(Vector3 position)
    {
        CapsuleCollider capsule = stalkerBody.GetComponent<CapsuleCollider>();
        // Get the bounds of the capsule collider
        Bounds bounds = new Bounds(position, capsule.bounds.size);

        // Get all colliders in the scene
        Collider[] allColliders = Physics.OverlapSphere(position, capsule.radius);

        // Check each collider to see if it's colliding with the capsule
        foreach (Collider collider in allColliders)
        {
            // Ignore the floor
            if (collider.CompareTag("Floor"))
                continue;
            // Ignore the capsule itself
            if (collider == capsule)
                continue;

            // If the bounds of the capsule intersect with the bounds of the collider, return true
            if (bounds.Intersects(collider.bounds))
                return true;
        }

        // If no collisions were found, return false
        return false;
    }

}
