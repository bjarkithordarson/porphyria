using System.Collections;
using UnityEngine;

public class StalkerInstantLungeState : StalkerBaseState
{
    public float speedMultiplier = 2;
    private float oldSpeedMultiplier;
    public override void EnterState(StalkerStateManager stalker)
    {
        stalker.controller.isSpawned = true;
        StalkerAudioManager.instance.PlayLungingEnter();
        Debug.Log("Stalker is instant lunging!");

        oldSpeedMultiplier = stalker.controller.lungeSpeedMultiplier;
        stalker.controller.lungeSpeedMultiplier = speedMultiplier;
        stalker.controller.RunTo(stalker.target.transform.position);
        stalker.controller.LookAt(stalker.target.transform.position);
        stalker.controller.StartFloatingAnimation();
    }
    public override void UpdateState(StalkerStateManager stalker)
    {
        if (Vector3.Distance(stalker.controller.stalkerBody.transform.position, stalker.controller.destination) < 0.5f)
        {
            stalker.controller.lungeSpeedMultiplier = oldSpeedMultiplier;
            stalker.TransitionToState(stalker.idleState);
        }
    }
    public override void OnTriggerEnterState(StalkerStateManager stalker, Collider other)
    {

    }
    public override void OnTriggerStayState(StalkerStateManager stalker, Collider other)
    {

    }
    public override void OnTriggerExitState(StalkerStateManager stalker, Collider other)
    {

    }
}
