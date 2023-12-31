// using System.Net.Http.Headers;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.AI;

// public class EnemySpawner : MonoBehaviour {
//     [Header("Settings")]
//     [SerializeField] private int enemyCount = 5;
//     [SerializeField] private  float spawnDelay = 1f;

//     [Header("References")]
//     [SerializeField] private Transform player;
//     [SerializeField] private List<Enemy> enemyPrefabs = new List<Enemy>();
//     [SerializeField] private SpawnMethod enemySpawnMethod = SpawnMethod.RoundRobin;

//     private NavMeshTriangulation triangulation;
//     private Dictionary<int, ObjectPool> enemyObjectPools = new Dictionary<int, ObjectPool>();

//     private void Awake() {
//         for (int i = 0; i < enemyPrefabs.Count; i++) {
//             enemyObjectPools.Add(i, ObjectPool.CreateInstance(enemyPrefabs[i], enemyCount));
//         }
//     }

//     private void Start() {
//         triangulation = NavMesh.CalculateTriangulation();
//         StartCoroutine(SpawnEnemies());
//     }

//     private IEnumerator SpawnEnemies() {
//         WaitForSeconds wait = new WaitForSeconds(spawnDelay);

//         int spawnedEnemies = 0;

//         while (spawnedEnemies < enemyCount) {
//             if (enemySpawnMethod == SpawnMethod.RoundRobin) {
//                 SpawnRoundRobinEnemy(spawnedEnemies);
//             } 
            
//             else if (enemySpawnMethod == SpawnMethod.Random) {
//                 SpawnRandomEnemy();
//             }

//             spawnedEnemies++;

//             yield return wait;
//         }
//     }

//     private void SpawnRoundRobinEnemy(int spawnedEnemies) {
//         int spawnIndex = spawnedEnemies % enemyPrefabs.Count;
//         DoSpawnEnemy(spawnIndex);      
//     }

//     private void SpawnRandomEnemy() {
//         DoSpawnEnemy(Random.Range(0, enemyPrefabs.Count));
//     }

//     private void DoSpawnEnemy(int spawnIndex) {
//         PoolableObject poolableObject = enemyObjectPools[spawnIndex].GetObject();

//         if (poolableObject != null) {
//             Enemy enemy = poolableObject.GetComponent<Enemy>();

//             int vertexIndex = Random.Range(0, triangulation.vertices.Length);

//             NavMeshHit hit;

//             if (NavMesh.SamplePosition(triangulation.vertices[vertexIndex], out hit, 2f, -1)) {
//                 enemy.enemyReferences.navMeshAgent.Warp(hit.position);
//                 enemy.enemyReferences.navMeshAgent.enabled = true;
//             }
//         } 

//         else {
//             Debug.LogError($"Unable to fetch enemy of type {spawnIndex} from object pool. Are you out of objects?");
//         }
//     }

//     public enum SpawnMethod {
//         RoundRobin,
//         Random
//     }
    
// }

// // Code modified from: https://www.youtube.com/watch?v=5uO0dXYbL-s
