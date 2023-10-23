using UnityEngine;

public interface IState {
    void Tick();
    void OnEnter();
    void OnExit();
    Color GizmoColor();
}

// Code modified from: https://game.courses/bots-ai-statemachines/