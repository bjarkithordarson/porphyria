using System;
using UnityEngine;

public class StalkerIdleState : StalkerBaseState
{
    public bool hasSeenPlayer = false;
    public float despawnTimeout = 10f;


    [SerializeField]
    private float timeLeft;
    public override void EnterState(StalkerStateManager stalker)
    {
        stalker.controller.isSpawned = true;
        Debug.Log("Stalker is idling.");
        StalkerAudioManager.instance.PlayIdleEnter();
        stalker.controller.StartIdleAnimation();
        hasSeenPlayer = false;
        stateEnteredTime = DateTime.Now;

        timeLeft = despawnTimeout;
        Debug.Log("Stalker is idle!");
    }
    public override void UpdateState(StalkerStateManager stalker)
    {

        stalker.controller.LookAt(stalker.target.transform.position);
        if(stalker.enableLunge && !hasSeenPlayer && stalker.controller.CanSee(stalker.target.transform.position))
        {
            hasSeenPlayer = true;
            stalker.TransitionToState(stalker.preparingLungeState);
        }

        if(stalker.controller.VisibleByCamera() == false)
        {
            timeLeft = despawnTimeout - (float)(DateTime.Now - stateEnteredTime).TotalSeconds;
        }

        if(timeLeft < 0)
        {
            stalker.TransitionToState(stalker.despawnedState);
            timeLeft = despawnTimeout;
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
