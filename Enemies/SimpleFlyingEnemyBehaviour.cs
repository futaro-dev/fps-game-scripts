// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using TMPro;

// public class SimpleFlyingEnemyBehaviour : MonoBehaviour, IDamageable {
//     [Header("Health")]
//     [SerializeField] private float health;
//     [SerializeField] private HealthBar healthBar;
//     [SerializeField] private TextMeshProUGUI healthText;
//     private float maxHealth;

//     [Header("Death")]
//     [SerializeField] private ParticleSystem explosionEffect;
//     [SerializeField] private AudioSource explosionSound;

//     private void Awake() {
//         maxHealth = health;
//     }

//     private void Update() {
//         HandleHealthText();
//     }

//     public void TakeDamage(float damage) {
//         health -= damage;

//         if (health <= 0) {
//             OnDeath();
//         }
//     }

//     public void OnDeath() {
//         Instantiate(explosionEffect, this.gameObject.transform.position, Quaternion.identity);
//         explosionSound.Play();
//         this.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
//         this.gameObject.GetComponent<SphereCollider>().enabled = false;
//         Destroy(gameObject, 5f);
//         Destroy(healthBar.gameObject, 0.1f);
//     }

//     private void HandleHealthText() {
//         if (health > 0) {
//             healthText.SetText(health + " / " + maxHealth);
//         } else {
//             healthText.SetText(0 + " / " + maxHealth);
//         }
//     }
// }
