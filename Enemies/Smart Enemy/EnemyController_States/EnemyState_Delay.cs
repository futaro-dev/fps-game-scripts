using System.Threading;
using UnityEngine;

public class EnemyState_Delay : IState {
    private float waitForSeconds;
    private float deadline;

    public EnemyState_Delay(float waitForSeconds) {
        this.waitForSeconds = waitForSeconds;
    }

    public void OnEnter() {
        deadline = Time.time + waitForSeconds;
    }

    public void OnExit() {

    }

    public void Tick() {
        
    }

    public Color GizmoColor() {
        return Color.white;
    }

    public bool IsDone() {
        return Time.time >= deadline;
    }
}
