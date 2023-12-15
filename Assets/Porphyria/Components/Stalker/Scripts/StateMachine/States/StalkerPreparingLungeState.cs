using System;
using System.Collections;
using UnityEngine;

public class StalkerPreparingLungeState : StalkerBaseState
{
    public float lungingTimeout = 7f;

    [SerializeField]
    private float timeLeft;
    public override void EnterState(StalkerStateManager stalker)
    {
        StalkerAudioManager.instance.PlayPreparingLungeEnter();
        Debug.Log("Stalker is preparing to lunge!");

        timeLeft = lungingTimeout;
    }

    public override void UpdateState(StalkerStateManager stalker)
    {
        if(!stalker.controller.CanSee(stalker.target.transform.position))
        {
            stalker.TransitionToState(stalker.idleState);
        }
        else
        {
            timeLeft = lungingTimeout - (float)(DateTime.Now - stateEnteredTime).TotalSeconds;
        }

        if(timeLeft <= 0)
        {
            stalker.TransitionToState(stalker.lungingState);
        }

    }
    public override void OnTriggerEnter(StalkerStateManager stalker, Collider other)
    {
        if(other.CompareTag("AntiStalkerLight"))
        {
            stalker.TransitionToState(stalker.scaredState);
        }
    }
    public override void OnTriggerStay(StalkerStateManager stalker, Collider other)
    {

    }
    public override void OnTriggerExit(StalkerStateManager stalker, Collider other)
    {

    }
}
