using UnityEngine;

public class EnemyState_Cover_RunToCover : IState {
    private EnemyReferences enemyReferences;
    private CoverArea coverArea;

    public EnemyState_Cover_RunToCover(EnemyReferences enemyReferences, CoverArea coverArea) {
        this.enemyReferences = enemyReferences;
        this.coverArea = coverArea;
    }

    public void OnEnter() {
        CoverWaypoint nextCover = this.coverArea.GetClosestCover(enemyReferences.transform.position);
        enemyReferences.navMeshAgent.SetDestination(nextCover.transform.position);
    }

    public void OnExit() {
        enemyReferences.animator.SetFloat("speed", 0f);
    }

    public void Tick() {
        enemyReferences.animator.SetFloat("speed", enemyReferences.navMeshAgent.desiredVelocity.sqrMagnitude);
    }

    public Color GizmoColor() {
        return Color.magenta;
    }

    public bool HasArrivedAtDestination() {
        return enemyReferences.navMeshAgent.remainingDistance < 0.1f;
    }
}

