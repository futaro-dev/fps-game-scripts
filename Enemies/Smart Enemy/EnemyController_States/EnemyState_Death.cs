using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Death : IState {
    private EnemyReferences enemyReferences;

    public EnemyState_Death(EnemyReferences enemyReferences) {
        this.enemyReferences = enemyReferences;
    }

    public void OnEnter() {
        enemyReferences.animator.SetTrigger("dead");
        enemyReferences.navMeshAgent.enabled = false;
        enemyReferences._collider.enabled = false;
        enemyReferences.healthBar.gameObject.SetActive(false);
        enemyReferences.deathAudio.GetComponent<AudioSource>().Play();

        
    }

    public void OnExit() {
        
    }

    public void Tick() {
        
    }

    public Color GizmoColor() {
        return Color.black;
    }
}
