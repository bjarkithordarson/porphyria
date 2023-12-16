using System;
using UnityEngine;

public class StalkerScaredState : StalkerBaseState
{
    public float cooldown = 2f;

    [SerializeField]
    private float timeLeft;
    public override void EnterState(StalkerStateManager stalker)
    {
        StalkerAudioManager.instance.PlayScaredEnter();
        timeLeft = cooldown;

    }
    public override void UpdateState(StalkerStateManager stalker)
    {
        timeLeft = cooldown- (float)(DateTime.Now - stateEnteredTime).TotalSeconds;


    }
    public override void OnTriggerEnterState(StalkerStateManager stalker, Collider other)
    {
        if (timeLeft < 0)
        {
            stalker.TransitionToState(stalker.preparingLungeState);
        }
    }
    public override void OnTriggerStayState(StalkerStateManager stalker, Collider other)
    {

    }
    public override void OnTriggerExitState(StalkerStateManager stalker, Collider other)
    {

    }
}
