using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Weapon/Gun")]

public class GunData : ScriptableObject {
    [Header("Info")]
    public new string name;

    [Header("Shooting")]
    public float damage;
    public float maxDistance;

    [Header("Reloading")]
    public int currentAmmo;
    public int magazineSize;
    public float fireRate;
    public float reloadTime;
    public bool reloading;
}

// Code modified from: https://www.youtube.com/watch?v=kXbQMhwj5Uc
