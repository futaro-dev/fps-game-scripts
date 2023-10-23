using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour {
    [Header("Settings")]
    [SerializeField] private int startingHealth = 100;
    [SerializeField] private int currentHealth;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI healthDisplay;

    private void Awake() {
        currentHealth = startingHealth;
    }

    public IEnumerator TakeDamage(int damage) {
        while (currentHealth > 0) {
            currentHealth -= damage;

            healthDisplay.SetText(currentHealth.ToString());

            yield return null;
        }
    }
}
