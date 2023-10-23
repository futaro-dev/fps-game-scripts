using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Shoot : IState {
    private EnemyReferences enemyReferences;
    private Transform target;

    public EnemyState_Shoot(EnemyReferences enemyReferences) {
        this.enemyReferences = enemyReferences;
    }

    public void OnEnter() {
        target = GameObject.FindWithTag("Player").transform;
    }

    public void OnExit() {
        enemyReferences.animator.SetBool("shooting", false);
        target = null;
    }

    public void Tick() {
        // TODO: Implement a more in-depth targeting system
        if (target != null) {
            Vector3 lookPosition = target.position - enemyReferences.transform.position;
            lookPosition.y = 0;
            
            Quaternion rotation = Quaternion.LookRotation(lookPosition);
            enemyReferences.transform.rotation = Quaternion.Slerp(enemyReferences.transform.rotation, rotation, 0.2f);

            // Decide to shoot or hide. For now, shoot first.
            enemyReferences.animator.SetBool("shooting", true);
        }
    }

    public Color GizmoColor() {
        return Color.red;
    }
}
