using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerController : MonoBehaviour
{
    public CapsuleCollider stalkerCollider;
    public GameObject stalkerBody;

    public Animator animator;

    public float lungeSpeed = 2;

    private Vector3 destination;
    void Start()
    {
        destination = stalkerBody.transform.position;
        animator = stalkerBody.GetComponent<Animator>();

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
        if (stalkerBody.transform.position != destination)
            stalkerBody.transform.position = Vector3.Lerp(stalkerBody.transform.position, destination, 0.01f);
        else
            animator.SetTrigger("Idle");
    }

    public void Spawn()
    {
        stalkerBody.SetActive(true);
    }
    public void Despawn()
    {
        stalkerBody.SetActive(false);
    }
    public void TeleportTo(Vector3 position)
    {
        stalkerBody.transform.position = position;
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

        if (Physics.Raycast(origin, direction, out hit))
            return hit.collider.CompareTag("Player");

        return false;
    }

    public void RunTo(Vector3 position)
    {
        animator.SetTrigger("Thriller");
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
}
