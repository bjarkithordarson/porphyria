using System.Collections;
using UnityEngine;

public class StalkerSpawningState : StalkerBaseState
{
    public float spawnRadius = 5f;
    public int minSpawnAngle = 0;
    public int maxSpawnAngle = 180;
    public float collisionCheckRadius = 1f;
    public int maxRetrySpawn = 100;

    private StalkerStateManager stalker;
    public override void EnterState(StalkerStateManager stalker)
    {
        StalkerAudioManager.instance.PlaySpawningEnter();
        this.stalker = stalker;

        Vector3 candidatePosition = stalker.controller.GetRandomPositionInRadius(
            stalker.target.transform.position,
            spawnRadius,
            minSpawnAngle,
            maxSpawnAngle,
            stalker.target.transform.eulerAngles.y);

        if(stalker.controller.IsCapsuleColliding(candidatePosition))
        {
            Debug.Log("Failed to spawn");
            stalker.TransitionToState(stalker.despawnedState);
        } else
        {
            stalker.controller.TeleportTo(candidatePosition);
            stalker.controller.Spawn();
            stalker.TransitionToState(stalker.idleState);
        }

    }
    public override void UpdateState(StalkerStateManager stalker)
    {

    }
    public override void OnTriggerEnter(StalkerStateManager stalker, Collider other)
    {

    }
    public override void OnTriggerStay(StalkerStateManager stalker, Collider other)
    {

    }
    public override void OnTriggerExit(StalkerStateManager stalker, Collider other)
    {

    }
}
