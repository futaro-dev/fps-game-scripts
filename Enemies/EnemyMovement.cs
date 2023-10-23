using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour {
    [Header("Settings")]
    public float updateRate = 0.2f;
    
    [Header("References")]
    public Transform player;
    private NavMeshAgent agent;

    private Coroutine followCoroutine;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
    }

    public void StartChasing() {
        if (followCoroutine == null) {
            followCoroutine = StartCoroutine(FollowTarget());
        }
    }

    private IEnumerator FollowTarget() {
        WaitForSeconds Wait = new WaitForSeconds(updateRate);

        while (gameObject.activeSelf) {
            agent.SetDestination(player.transform.position);
            yield return Wait;
        }
    }
}