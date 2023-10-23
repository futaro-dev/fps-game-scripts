using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
public class EnemyReferences : MonoBehaviour {
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public Animator animator;
    [HideInInspector] public EnemyShooter shooter;
    [HideInInspector] public CapsuleCollider _collider;
    [HideInInspector] public GameObject healthBar;
    [HideInInspector] public GameObject deathAudio;

    [Header("Settings")]
    public float pathUpdateDelay = 0.2f;

    private void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        shooter = GetComponent<EnemyShooter>();
        _collider = GetComponent<CapsuleCollider>();
        healthBar = transform.GetChild(1).gameObject;
        deathAudio = transform.GetChild(2).gameObject;
    }
}
