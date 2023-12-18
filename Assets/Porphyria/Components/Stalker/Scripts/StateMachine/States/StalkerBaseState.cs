using System;
using UnityEngine;

public abstract class StalkerBaseState : MonoBehaviour
{
    public DateTime stateEnteredTime;
    public void StartTiming()
    {
        stateEnteredTime = DateTime.Now;
    }
    public abstract void EnterState(StalkerStateManager stalker);
    public abstract void UpdateState(StalkerStateManager stalker);
    public abstract void OnTriggerEnterState(StalkerStateManager stalker, Collider other);
    public abstract void OnTriggerStayState(StalkerStateManager stalker, Collider other);
    public abstract void OnTriggerExitState(StalkerStateManager stalker, Collider other);
}
