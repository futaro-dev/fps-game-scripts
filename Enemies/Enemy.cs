using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class Enemy : MonoBehaviour, IDamageable {
    [Header("Health")]
    [SerializeField] private float health;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private TextMeshProUGUI healthText;
    private float maxHealth;

    // [Header("Death")]
    // [SerializeField] private ParticleSystem explosionEffect;
    // [SerializeField] private AudioSource explosionSound;

    [HideInInspector] public EnemyReferences enemyReferences;

    private void Awake() {
        maxHealth = health;
        UpdateText();
    }

    // public override void OnDisable() {
    //     base.OnDisable();
    //     if (enemyReferences.navMeshAgent) {
    //         enemyReferences.navMeshAgent.enabled = false;
    //     }
    // }

    public void TakeDamage(float damage) {
        health -= damage;

        if (health > 0) {
            healthBar.SetHealth(health / maxHealth, 3);
        } 

        else {
            healthBar.SetHealth(0, 3);
        }

        UpdateText();
    }

    public bool TakenDamage() {
        if (health < maxHealth) {
            return true;
        }

        return false;
    }

    private void UpdateText() {
        if (health > 0) {
            healthText.SetText(health + " / " + maxHealth);
        } 
        
        else {
            healthText.SetText(0 + " / " + maxHealth);
        }
    }

    public bool HasDied() {
        if (health <= 0) {
            return true;
        }

        return false;
    }

    // public IEnumerator OnDeath() {
    //     // Create the explosion
    //     Instantiate(explosionEffect, this.gameObject.transform.position, Quaternion.identity);
    //     explosionSound.Play();

    //     // Disable the collision/renderer
    //     this.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;

    //     if (this.gameObject.GetComponent<CapsuleCollider>()) {
    //         this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
    //     }

    //     if (this.gameObject.GetComponent<SphereCollider>()) {
    //         this.gameObject.GetComponent<SphereCollider>().enabled = false;
    //     }

    //     healthBar.gameObject.SetActive(false);

    //     yield return new WaitForSeconds(3f);

    //     // Disable the game object
    //     gameObject.SetActive(false);
    // }
}
