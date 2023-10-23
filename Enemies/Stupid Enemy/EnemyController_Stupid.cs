using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_Stupid : MonoBehaviour {
    public Transform target;

    private EnemyReferences enemyReferences;

    private float pathUpdateDeadline;

    private float shootingDistance;

    private void Awake() {
        enemyReferences = GetComponent<EnemyReferences>();
    }

    private void Start() {
        shootingDistance = enemyReferences.navMeshAgent.stoppingDistance;
    }

    private void Update() {
        if (target != null) {
            bool inRange = Vector3.Distance(transform.position, target.position) <= shootingDistance;

            if (inRange) {
                LookAtTarget();
            }

            else {
                UpdatePath();
            }

            enemyReferences.animator.SetBool("shooting", inRange);
        }

        enemyReferences.animator.SetFloat("speed", enemyReferences.navMeshAgent.desiredVelocity.sqrMagnitude);
    }

    private void LookAtTarget() {
        Vector3 lookPosition = target.position - transform.position;
        lookPosition.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPosition);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);
    }

    private void UpdatePath() {
        // Adding a delay so that the destination is not set on every frame
        if (Time.time >= pathUpdateDeadline) {
            Debug.Log("Updating path");
            pathUpdateDeadline = Time.time + enemyReferences.pathUpdateDelay;
            enemyReferences.navMeshAgent.SetDestination(target.position);
        }
    }
}

// Code modified from: https://www.youtube.com/watch?v=rs7xUi9BqjE
