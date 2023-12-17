using System;
using System.Collections;
using UnityEngine;

public class StalkerPreparingLungeState : StalkerBaseState
{
    [Header("Knowledge")]
    public bool seesTarget = false;
    public float distanceToTarget;
    [Header("Timer")]
    public float lungingTimeout = 7f;
    private float timeLeft;

    [Header("Regular lunge")]
    public float criticalHitDistance = 1f;
    public float maxLungeDistance = 10f;

    [Header("Instant lunge")]
    public bool enableInstantLunge = true;
    public float instantLungeDistance = 2.5f;

    [SerializeField]
    public override void EnterState(StalkerStateManager stalker)
    {
        stalker.controller.isSpawned = true;
        StalkerAudioManager.instance.PlayPreparingLungeEnter();
        stalker.controller.StartIdleAnimation();
        Debug.Log("Stalker is preparing to lunge!");

        timeLeft = lungingTimeout;
    }

    public override void UpdateState(StalkerStateManager stalker)
    {
        if(GameManager.instance.isPaused)
        {
            stateEnteredTime = DateTime.Now;
            StalkerAudioManager.instance.Mute();
            return;
        } else
        {
            StalkerAudioManager.instance.Unmute();
        }
        distanceToTarget = stalker.controller.GetDistanceTo(stalker.target.transform.position);
        seesTarget = stalker.controller.CanSee(stalker.target.transform.position);

        if(!seesTarget)
        {
            stalker.TransitionToState(stalker.idleState);
        }

        // Count down to lunge.
        if(distanceToTarget < maxLungeDistance)
        {
            timeLeft = lungingTimeout - (float)(DateTime.Now - stateEnteredTime).TotalSeconds;
            if(timeLeft <= 0)
            {
                stalker.TransitionToState(stalker.lungingState);
                timeLeft = maxLungeDistance;
}
        } else
        {
            timeLeft = lungingTimeout;
            stateEnteredTime = DateTime.Now;
        }

        // Instant lunge
        if(enableInstantLunge && distanceToTarget < instantLungeDistance)
        {
            stalker.TransitionToState(stalker.lungingState);
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
