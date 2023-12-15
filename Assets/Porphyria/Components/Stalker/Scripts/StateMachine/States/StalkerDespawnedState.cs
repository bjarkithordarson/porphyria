using System;
using System.Collections;
using UnityEngine;

public class StalkerDespawnedState : StalkerBaseState
{
    public float spawnTimeout = 10f;
    private StalkerStateManager stalker;

    public float timeLeft;
    public override void EnterState(StalkerStateManager stalker)
    {
        StalkerAudioManager.instance.PlayDespawnedEnter();
        timeLeft = spawnTimeout;
        this.stalker = stalker;
        stalker.controller.Despawn();
    }

    public override void UpdateState(StalkerStateManager stalker)
    {
        timeLeft = spawnTimeout - (float)(DateTime.Now - stateEnteredTime).TotalSeconds;

        if(timeLeft < 0)
        {
            stalker.TransitionToState(stalker.spawningState);
        }
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
