using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyHealth : MonoBehaviour, IDamageable {
    [Header("Settings")]
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float health;

    [Header("UI")]
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private TextMeshProUGUI healthText;

    private void Awake() {
        health = maxHealth;
        UpdateHealth();
    }

    public void TakeDamage(float damage) {
        if (health > 0) {
            healthBar.SetHealth(health / maxHealth, 3);
            UpdateHealth();
        } 

        else {
            healthBar.SetHealth(0, 3);
        }
    }

    public void UpdateHealth() {
        healthText.SetText(health + " / " + maxHealth);
    }
}
