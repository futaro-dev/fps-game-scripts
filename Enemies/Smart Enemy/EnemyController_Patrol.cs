using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController_Patrol : MonoBehaviour {
    private Enemy enemy;
    private EnemyReferences enemyReferences;
    private StateMachine stateMachine;
    [SerializeField] private PatrolArea patrolArea;
    
    
    private void Start() {
        enemy = GetComponent<Enemy>();
        enemyReferences = GetComponent<EnemyReferences>();
        stateMachine = new StateMachine();

        // STATES
        var patrol = new EnemyState_Patrol(enemyReferences, patrolArea);
        var chasePlayer = new EnemyState_ChasePlayer(enemyReferences);
        var shoot = new EnemyState_Shoot(enemyReferences);
        var reload = new EnemyState_Reload(enemyReferences);
        var death = new EnemyState_Death(enemyReferences);

        // TRANSITIONS
        At(patrol, chasePlayer, () => enemy.TakenDamage());

        At(chasePlayer, shoot, () => chasePlayer.InShootingDistance());
        At(shoot, chasePlayer, () => !chasePlayer.InShootingDistance());
        
        Any(reload, () => enemyReferences.shooter.ShouldReload());

        At(reload, shoot, () => chasePlayer.InShootingDistance());
        At(reload, chasePlayer, () => !chasePlayer.InShootingDistance());

        Any(death, () => enemy.HasDied());

        // START STATE
        stateMachine.SetState(patrol);

        // FUNCTIONS & CONDITIONS
        void At(IState from, IState to, Func<bool> condition) => stateMachine.AddTransition(from, to, condition);
        void Any(IState to, Func<bool> condition) => stateMachine.AddAnyTransition(to, condition);  
    }

    private void Update() {
        stateMachine.Tick();
    }

    private void OnDrawGizmos() {
        if (stateMachine != null) {
            Gizmos.color = stateMachine.GetGizmoColor();
            Gizmos.DrawSphere(transform.position + Vector3.up * 3, 0.4f);
        }
    }
}

// Code modified from: https://www.youtube.com/watch?v=rs7xUi9BqjE
