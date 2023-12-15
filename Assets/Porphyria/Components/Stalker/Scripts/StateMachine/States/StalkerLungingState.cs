using System.Collections;
using UnityEngine;

public class StalkerLungingState : StalkerBaseState
{
    public float timeBeforeLunge = 1f;
    public float returnToIdleTimeout = 3f;
    public override void EnterState(StalkerStateManager stalker)
    {
        StalkerAudioManager.instance.PlayLungingEnter();
        Debug.Log("Stalker is lunging!");
        //StartCoroutine(Lunge(stalker));
        stalker.controller.RunTo(stalker.target.transform.position);
        stalker.controller.LookAt(stalker.target.transform.position);
        stalker.controller.StartFloatingAnimation();
    }

    private IEnumerator Lunge(StalkerStateManager stalker)
    {
        yield return new WaitForSeconds(timeBeforeLunge);
        
    }

    public override void UpdateState(StalkerStateManager stalker)
    {
        if(Vector3.Distance(stalker.controller.stalkerBody.transform.position, stalker.controller.destination) < 0.5f)
        {
            stalker.TransitionToState(stalker.idleState);
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
