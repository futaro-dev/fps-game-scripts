using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Patrol : IState {
    private EnemyReferences enemyReferences;
    private PatrolArea patrolArea;
    private PatrolRoute patrolRoute;
    private StateMachine stateMachine;

    public EnemyState_Patrol(EnemyReferences enemyReferences, PatrolArea patrolArea) {
        this.enemyReferences = enemyReferences;
        this.patrolArea = patrolArea;

        // Creating a new sub-state machine
        stateMachine = new StateMachine();

        // STATES
        var walkToNextPoint = new EnemyState_Patrol_WalkToNextPoint(enemyReferences, patrolArea.GetRandomPatrolRoute(), patrolArea.GetStartingPatrolWaypoint());
        var enemyDelay = new EnemyState_Delay(5f);

        // TRANSITIONS
        At(walkToNextPoint, enemyDelay, () => walkToNextPoint.HasArrivedAtDestination());
        At(enemyDelay, walkToNextPoint, () => enemyDelay.IsDone());

        // START STATE
        stateMachine.SetState(walkToNextPoint);

        // FUNCTIONS & CONDITIONS
        void At(IState from, IState to, Func<bool> condition) => stateMachine.AddTransition(from, to, condition);
        // void Any(IState to, Func<bool> condition) => stateMachine.AddAnyTransition(to, condition);
    }

    public void OnEnter() {
        enemyReferences.navMeshAgent.speed = 2f;
        enemyReferences.navMeshAgent.stoppingDistance = 0f;
    }

    public void OnExit() {
        enemyReferences.animator.SetFloat("speed", 0f);
        enemyReferences.navMeshAgent.stoppingDistance = 8f;
    }

    public void Tick() {
        stateMachine.Tick();
    }

    public Color GizmoColor() {
        return Color.cyan;
    }
}
