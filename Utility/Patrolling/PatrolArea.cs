using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolArea : MonoBehaviour {
    private PatrolWaypoint startingWaypoint;
    private PatrolRoute[] routes;

    private void Awake() {
        startingWaypoint = GetComponentInChildren<PatrolWaypoint>();
        routes = GetComponentsInChildren<PatrolRoute>();
    }

    public PatrolWaypoint GetStartingPatrolWaypoint() {
        return startingWaypoint;
    }

    public PatrolRoute GetRandomPatrolRoute() {
        return routes[Random.Range(0, routes.Length)];
    }
}
