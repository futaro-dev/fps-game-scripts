using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_ChasePlayer : IState {
    private EnemyReferences enemyReferences;
    private float shootingDistance;
    private Transform target;

    public EnemyState_ChasePlayer(EnemyReferences enemyReferences) {
        this.enemyReferences = enemyReferences;
    }

    public void OnEnter() {
        shootingDistance = enemyReferences.navMeshAgent.stoppingDistance;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        enemyReferences.navMeshAgent.speed = 4.5f;
    }

    public void OnExit() {
        enemyReferences.animator.SetFloat("speed", 0f);
    }

    public void Tick() {
        enemyReferences.animator.SetFloat("speed", enemyReferences.navMeshAgent.desiredVelocity.sqrMagnitude);
        enemyReferences.navMeshAgent.SetDestination(target.position);
    }

    public Color GizmoColor() {
        return Color.yellow;
    }

    public bool InShootingDistance() {
        return Vector3.Distance(enemyReferences.transform.position, target.position) <= shootingDistance;
    }
}
