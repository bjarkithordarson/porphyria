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
        StalkerAudioManager.instance.PlayIdleEnter();
        stalker.controller.StartIdleAnimation();
        hasSeenPlayer = false;

        timeLeft = despawnTimeout;
        Debug.Log("Stalker is idle!");
    }
    public override void UpdateState(StalkerStateManager stalker)
    {
        stalker.controller.LookAt(stalker.target.transform.position);
        if(hasSeenPlayer == false && stalker.controller.CanSee(stalker.target.transform.position))
        {
            hasSeenPlayer = true;
            stalker.TransitionToState(stalker.preparingLungeState);
        }

        Debug.Log(stalker.controller.VisibleByCamera());

        if(stalker.controller.VisibleByCamera() == false)
        {
            timeLeft = despawnTimeout - (float)(DateTime.Now - stateEnteredTime).TotalSeconds;
        }

        if(timeLeft < 0)
        {
            stalker.TransitionToState(stalker.despawnedState);
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
