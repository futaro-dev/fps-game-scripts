using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_Cover : MonoBehaviour {
    private Enemy enemy;
    private EnemyReferences enemyReferences;
    private StateMachine stateMachine;
    
    private void Start() {
        enemy = GetComponent<Enemy>();
        enemyReferences = GetComponent<EnemyReferences>();
        stateMachine = new StateMachine();
        
        CoverArea coverArea = FindObjectOfType<CoverArea>();

        // STATES
        var idle = new EnemyState_Idle(enemyReferences);
        var runToCover = new EnemyState_Cover_RunToCover(enemyReferences, coverArea);
        var delayAfterRun = new EnemyState_Delay(0.5f);
        var cover = new EnemyState_Cover(enemyReferences);
        var death = new EnemyState_Death(enemyReferences);

        // TRANSITIONS
        At(idle, runToCover, () => enemy.TakenDamage());
        At(runToCover, delayAfterRun, () => runToCover.HasArrivedAtDestination());
        At(delayAfterRun, cover, () => delayAfterRun.IsDone());
        Any(death, () => enemy.HasDied());

        // START STATE
        stateMachine.SetState(idle);

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
