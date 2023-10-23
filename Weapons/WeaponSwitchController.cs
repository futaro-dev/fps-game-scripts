using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitchController : MonoBehaviour {
    [Header("Keys")]
    [SerializeField] private KeyCode[] keys;

    [Header("Settings")]
    [SerializeField] private float switchTime;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI statusDisplay;
    [SerializeField] private Image primaryHighlighted;
    [SerializeField] private Image secondaryHighlighted;

    [Header("References")]
    [SerializeField] private Transform[] weapons;

    private int selectedWeapon;
    private float timeSinceLastSwitch;

    private void Start() {
        SetWeapons();
        Select(selectedWeapon);

        timeSinceLastSwitch = 0f;
    }

    private void SetWeapons() {
        weapons = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++) {
            weapons[i] = transform.GetChild(i);
        }

        if (keys == null) keys = new KeyCode[weapons.Length];
    }

    private void Update() {
        int previousSelectedWeapon = selectedWeapon;

        for (int i = 0; i < keys.Length; i++) {
            if (Input.GetKeyDown(keys[i]) && timeSinceLastSwitch >= switchTime) {
                selectedWeapon = i;
            }
        }

        if (previousSelectedWeapon != selectedWeapon) {
            Select(selectedWeapon);
        }

        HandleHighlighted(selectedWeapon);

        timeSinceLastSwitch += Time.deltaTime;
    }

    private void Select(int weaponIndex) {
        for (int i = 0; i < weapons.Length; i++) {
            weapons[i].gameObject.SetActive(i == weaponIndex);
        }

        timeSinceLastSwitch = 0f;

        OnWeaponSelected();
    }

    // Utility function to handle the UI
    private void HandleHighlighted(int selectedWeapon) {
        if (selectedWeapon == 0) {
            primaryHighlighted.enabled = true;
            secondaryHighlighted.enabled = false;
        } else if (selectedWeapon == 1) {
            primaryHighlighted.enabled = false;
            secondaryHighlighted.enabled = true;
        }
    }

    private void OnWeaponSelected() {
        statusDisplay.SetText("");
    }
}

// Code modified from: https://www.youtube.com/watch?v=kasbsBho9ZM
