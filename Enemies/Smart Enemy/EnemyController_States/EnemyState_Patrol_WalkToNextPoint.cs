using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Patrol_WalkToNextPoint : IState {
    private EnemyReferences enemyReferences;
    private PatrolRoute patrolRoute;
    private PatrolWaypoint startingPoint;

    public EnemyState_Patrol_WalkToNextPoint(EnemyReferences enemyReferences, PatrolRoute patrolRoute, PatrolWaypoint startingPoint) {
        this.enemyReferences = enemyReferences;
        this.patrolRoute = patrolRoute;
        this.startingPoint = startingPoint;
    }

    public void OnEnter() {
        PatrolWaypoint nextPatrolWaypoint = patrolRoute.GetNextPatrolWaypoint(enemyReferences.transform.position, startingPoint);
        enemyReferences.navMeshAgent.SetDestination(nextPatrolWaypoint.transform.position);
    }

    public void OnExit() {
        enemyReferences.animator.SetFloat("speed", 0f);
    }

    public void Tick() {
        enemyReferences.animator.SetFloat("speed", enemyReferences.navMeshAgent.desiredVelocity.sqrMagnitude / 8);
    }

    public Color GizmoColor() {
        return Color.cyan;
    }

    public bool HasArrivedAtDestination() {
        return enemyReferences.navMeshAgent.remainingDistance < 0.1f;
    }
}
