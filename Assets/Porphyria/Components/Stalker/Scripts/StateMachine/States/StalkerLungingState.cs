using System.Collections;
using UnityEngine;

public class StalkerLungingState : StalkerBaseState
{
    public float timeBeforeLunge = 1f;
    public float returnToIdleTimeout = 3f;
    public override void EnterState(StalkerStateManager stalker)
    {
        Debug.Log("Stalker is lunging!");
        StartCoroutine(Lunge(stalker));
    }

    private IEnumerator Lunge(StalkerStateManager stalker)
    {
        yield return new WaitForSeconds(timeBeforeLunge);

        stalker.controller.RunTo(stalker.target.transform.position);
        stalker.TransitionToState(stalker.idleState);
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
