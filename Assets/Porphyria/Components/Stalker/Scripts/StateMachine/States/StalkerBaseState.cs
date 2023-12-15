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
    public abstract void OnTriggerEnter(StalkerStateManager stalker, Collider other);
    public abstract void OnTriggerStay(StalkerStateManager stalker, Collider other);
    public abstract void OnTriggerExit(StalkerStateManager stalker, Collider other);
}
