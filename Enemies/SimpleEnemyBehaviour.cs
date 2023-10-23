// using UnityEngine;
// using UnityEngine.AI;
// using TMPro;

// public class SimpleEnemyBehaviour : MonoBehaviour, IDamageable {
//         [Header("Health Configuration")]
//         [SerializeField] private float health;
//         [SerializeField] private HealthBar healthBar;
//         [SerializeField] private TextMeshProUGUI healthText;
//         private float maxHealth;

//         [Header("Patrol Configuration")]
//         public Vector3 wayPoint;
//         bool wayPointSet;
//         public float wayPointRange;

//         [Header("Attack Configuration")]
//         [SerializeField] private float timeBetweenAttacks;
//         // [SerializeField] private GameObject projectile;
//         private bool alreadyAttacked;

//         [Header("State Configuration")]
//         [SerializeField] private float sightRange, attackRange;
//         [SerializeField] private bool playerInSightRange, playerInAttackRange;
//         [SerializeField] private float pauseTime;
//         private float lastDidSomething;

//         [Header("Death Configuration")]
//         [SerializeField] private ParticleSystem explosionEffect;
//         [SerializeField] private AudioSource explosionSound;

//         [Header("References")]
//         public NavMeshAgent agent;
//         public Transform player;
//         public Transform attackPoint;
//         public LayerMask groundMask, playerMask;

//         private void Awake() {
//             player = GameObject.Find("Player/Body").transform;
//             agent = GetComponent<NavMeshAgent>();
//             maxHealth = health;
//         }

//         private void Update() {
//             if (this.gameObject.activeSelf) {
//                 playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerMask);
//                 playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);
            
//                 if (!playerInSightRange && !playerInAttackRange) {
//                     gameObject.GetComponentInChildren<Renderer>().material.color = Color.gray;
//                     Patrolling();
//                 }
//                 if (playerInSightRange && !playerInAttackRange) {
//                     gameObject.GetComponentInChildren<Renderer>().material.color = Color.yellow;
//                     Chasing();  
//                 } 
//                 if (playerInSightRange && playerInAttackRange) {
//                     gameObject.GetComponentInChildren<Renderer>().material.color = Color.red;
//                     Attacking();
//                 }

//                 HandleHealthText();
//             }
//         }

//         private void Patrolling() {
//             if (!wayPointSet) SearchWayPoint();

//             if (wayPointSet) {
//                 agent.SetDestination(wayPoint);
//             }

//             Vector3 distanceToWayPoint = transform.position - wayPoint;

//             // Waypoint has been reached
//             if (distanceToWayPoint.magnitude < 1f) {
//                 if (Time.time < lastDidSomething + pauseTime) return;
//                 wayPointSet = false;
//             }

//             lastDidSomething = Time.time;
//         }

//         private void SearchWayPoint() {
//             float randomZ = Random.Range(-wayPointRange, wayPointRange);
//             float randomX = Random.Range(-wayPointRange, wayPointRange);
            
//             wayPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

//             if (Physics.Raycast(wayPoint, -transform.up, 2f, groundMask)) {
//                 wayPointSet = true;
//             }
//         }

//         private void Chasing() {
//             agent.SetDestination(player.position);
//         }

//         private void Attacking() {
//             agent.SetDestination(transform.position);
            
//             transform.LookAt(player);

//             if (!alreadyAttacked) {

//                 // Attack code here
//                 // Rigidbody body = Instantiate(projectile, attackPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
//                 // body.AddForce(transform.forward * 30f, ForceMode.Impulse);
//                 // body.AddForce(transform.up * 2f, ForceMode.Impulse);
//                 //

//                 alreadyAttacked = true;
//                 Invoke(nameof(ResetAttack), timeBetweenAttacks);
//             }
//         }

//         private void ResetAttack() {
//             alreadyAttacked = false;
//         }

//         public void TakeDamage(float damage) {
//             health -= damage;

//             healthBar.SetHealth(health / maxHealth, 3);

//             if (health <= 0) {
//                 OnDeath(); 
//             }
//         }

//         public void OnDeath() {
//             Instantiate(explosionEffect, this.gameObject.transform.position, Quaternion.identity);
//             explosionSound.Play();
//             this.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
//             this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
//             Destroy(gameObject, 5f);
//             Destroy(healthBar.gameObject, 0.1f);
//         }

//         private void HandleHealthText() {
//             if (health > 0) {
//                 healthText.SetText(health + " / " + maxHealth);
//             } else {
//                 healthText.SetText(0 + " / " + maxHealth);
//             }
//         }

//         // Visualises the sight and attack range (for testing purposes)
//         private void OnDrawGizmosSelected() {
//             Gizmos.color = Color.red;
//             Gizmos.DrawWireSphere(transform.position, attackRange);
//             Gizmos.color = Color.yellow;
//             Gizmos.DrawWireSphere(transform.position, sightRange);
//         }
// }
