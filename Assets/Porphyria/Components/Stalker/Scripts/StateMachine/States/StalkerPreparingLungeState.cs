using System;
using System.Collections;
using UnityEngine;

public class StalkerPreparingLungeState : StalkerBaseState
{
    public float lungingTimeout = 7f;
    public float criticalHitDistance = 1f;
    public float maxLungeDistance = 10f;
    public float distanceToTarget;
    public bool seesTarget = false;

    [SerializeField]
    private float timeLeft;
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

        /*if (distanceToTarget <= criticalHitDistance)
        {
            stalker.TransitionToState(stalker.scaredState);
        }*/

        if(!seesTarget)
        {
            stalker.TransitionToState(stalker.idleState);
        }

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
