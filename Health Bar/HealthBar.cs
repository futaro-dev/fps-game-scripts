using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    [Header("Settings")]
    [SerializeField] private float defaultSpeed = 1f;

    [Header("References")]
    [SerializeField] private Image healthImage;

    private Coroutine animationCoroutine;

    private void Start() {
        if (healthImage.type != Image.Type.Filled) {
            Debug.LogError($"{name}'s progressImage is not of type \"Filled\" so it cannot be used" +
                $"as a progress bar. Disabling this progress bar");
            this.enabled = false;
        }
    }

    public void SetHealth(float health) {
        SetHealth(health, defaultSpeed);
    }

    public void SetHealth(float health, float speed) {
        if (health < 0 || health > 1) {
            health = Mathf.Clamp01(health);
        }

        if (health != healthImage.fillAmount) {
            if (animationCoroutine != null) {
                StopCoroutine(animationCoroutine);
            }

            animationCoroutine = StartCoroutine(AnimateProgress(health, speed));
        }
    }

    private IEnumerator AnimateProgress(float health, float speed) {
        float time = 0;
        float initialProgress = healthImage.fillAmount;

        while (time < 1) {
            healthImage.fillAmount = Mathf.Lerp(initialProgress, health, time);
            time += Time.deltaTime * speed;

            yield return null;
        }

        healthImage.fillAmount = health;
    }
}

// Code modified from: https://www.youtube.com/watch?v=Qw8odLHv38Q
