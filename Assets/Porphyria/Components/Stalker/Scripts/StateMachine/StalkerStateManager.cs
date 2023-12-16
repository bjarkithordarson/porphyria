using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerStateManager : MonoBehaviour
{
    [Header("State Machine")]
    [SerializeField]
    StalkerBaseState currentState;

    [Header("Controller")]
    public StalkerController controller;

    [Header("Properties")]
    public GameObject target;

    [Header("States")]
    public StalkerDespawnedState despawnedState;
    public StalkerSpawningState  spawningState;
    public StalkerIdleState      idleState;
    public StalkerScaredState    scaredState;
    public StalkerPreparingLungeState preparingLungeState;
    public StalkerLungingState   lungingState;

    void Start()
    {
        controller     = gameObject.GetComponentInParent<StalkerController>();
        despawnedState = gameObject.GetComponent<StalkerDespawnedState>() ?? gameObject.AddComponent<StalkerDespawnedState>();
        spawningState  = gameObject.GetComponent<StalkerSpawningState>()  ?? gameObject.AddComponent<StalkerSpawningState>();
        idleState      = gameObject.GetComponent<StalkerIdleState>()      ?? gameObject.AddComponent<StalkerIdleState>();
        scaredState    = gameObject.GetComponent<StalkerScaredState>()    ?? gameObject.AddComponent<StalkerScaredState>();
        preparingLungeState = gameObject.GetComponent<StalkerPreparingLungeState>() ?? gameObject.AddComponent<StalkerPreparingLungeState>();
        lungingState   = gameObject.GetComponent<StalkerLungingState>() ?? gameObject.AddComponent<StalkerLungingState>();

        TransitionToState(despawnedState);
    }

    void Update() => currentState.UpdateState(this);
    void OnTriggerEnter(Collider other) => currentState.OnTriggerEnterState(this, other);
    void OnTriggerStay(Collider other) => currentState.OnTriggerStayState(this, other);
    private void OnTriggerExit(Collider other) => currentState.OnTriggerExitState(this, other);

    public void TransitionToState(StalkerBaseState state)
    {
        currentState = state;
        state.EnterState(this);
        state.StartTiming();
        Debug.Log("Stalker transitioning to " + state);
    }
}
