using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PatrolRoute : MonoBehaviour {
    private PatrolWaypoint[] patrolsForward;
    private PatrolWaypoint[] patrolsBackwards;
    private PatrolWaypoint[] patrols;
    
    private int patrolWaypoint = 0;

    public bool routeFinished;

    private void Awake() {
        patrolsForward = GetComponentsInChildren<PatrolWaypoint>();
        patrolsBackwards = GetComponentsInChildren<PatrolWaypoint>();
        Array.Reverse(patrolsBackwards);
        patrols = patrolsForward.Concat(patrolsBackwards).ToArray();
    }

    public PatrolWaypoint GetNextPatrolWaypoint(Vector3 agentLocation, PatrolWaypoint startingWaypoint) {
        if (patrolWaypoint < patrols.Length) {
            routeFinished = false;

            PatrolWaypoint waypoint = patrols[patrolWaypoint];
            patrolWaypoint++;

            return waypoint;
        }
        
        routeFinished = true;
        
        patrolWaypoint = 0;
        return startingWaypoint;
    }
}

// tracker 1
// patrolwaypoint 1

// trackerr 2
// patrolwaypoint 2

// tracker 3
// patrolwaypoint 3

// tracker 4
// patrolwaypoint 2

// tracker 5
// patrolwaypoint 1

// tracker 6
// patrolwaypoin
