using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StalkerController : MonoBehaviour
{
    public CapsuleCollider stalkerCollider;
    public GameObject stalkerBody;

    public StalkerStateManager stateMachine;
    public bool seenByCamera = false;

    public float minDangerDistance = 5f;

    public ProximityDanger proximityDanger;

    private bool inDanger = false;

    private bool inSafeDistance = false;

    //public Animator animator;

    public float lungeSpeed = 2;

    public Vector3 destination;

    public bool isSpawned;
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
        //Debug.Log(VisibleByCamera());
        if (!seenByCamera && VisibleByCamera())
        {
            //Debug.Log("ASDFASDFASDF");
            seenByCamera = true;
            StalkerAudioManager.instance.PlayFirstSeenByCamera();
        }
        float distanceFromPlayer = Vector3.Distance(stateMachine.target.transform.position, transform.position);

        if(distanceFromPlayer < minDangerDistance && !inDanger && isSpawned) {
            //Debug.Log("Player is in danger ASDASDASDASDASDASDDDDDD");
            proximityDanger.PlayerDangerEffect();
            inDanger = true;
            inSafeDistance = true;
            
            

        } else if(distanceFromPlayer > minDangerDistance && inSafeDistance)
        {
            //Debug.Log("Player is not in danger ASDASDADASDASDASDASD");
            proximityDanger.PlayerSafe();
            inSafeDistance = false;
            inDanger = false;
             
            
        }

        Move();
    }

    private void Move()
    {
        if (Vector3.Distance(transform.position, destination) > 0.01f)
            transform.position = Vector3.Lerp(transform.position, destination, 0.01f * lungeSpeed);
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
        seenByCamera = false;
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
    public float GetDistanceTo(Vector3 position)
    {
        return Vector3.Distance(transform.position, position);
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
    public void StartScaredAnimation()
    {
        GetComponent<Animator>().Play("Scared");
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
        Bounds bounds = new Bounds(position, capsule.bounds.size);

        Collider[] allColliders = Physics.OverlapSphere(position, capsule.radius);

        foreach (Collider collider in allColliders)
        {
            if (collider.CompareTag("Floor"))
                continue;
            if (collider == capsule)
                continue;
            if (bounds.Intersects(collider.bounds))
                return true;
        }
        return false;
    }

    public StalkerBaseState GetState()
    {
        return stateMachine.currentState;
    }

    private void OnTriggerStay(Collider collider)
    {
        if(collider.CompareTag("Player") && isSpawned)
        {
            SceneManager.LoadScene("EndScene");

            Debug.Log("STALKER COLLIDING WITH PLAYER");
        }
    }

}
