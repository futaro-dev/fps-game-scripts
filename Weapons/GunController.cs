using System.Collections;
using TMPro;
using UnityEngine;

public class GunController : MonoBehaviour {
    [Header("Info")]
    [SerializeField] GunData gunData;
    
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI ammunitionDisplay;
    [SerializeField] private TextMeshProUGUI statusDisplay;

    [Header("Bullet Decal/Trail")]
    public Vector3 spread = new Vector3(0.06f, 0.06f, 0.06f);
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private TrailRenderer bulletTrail;
    [SerializeField] private ParticleSystem bulletDecal;
    [SerializeField] private LayerMask mask;

    [Header("Muzzle")]
    [SerializeField] private Transform muzzle;
    [SerializeField] private GameObject muzzleFlash;
    
    [Header("Audio")]
    [SerializeField] private AudioSource shootingAudio;
    [SerializeField] private AudioSource reloadingAudio;
    [SerializeField] private AudioSource hitSoundSource;
    [SerializeField] private AudioClip hitSound;

    [Header("References")]
    [SerializeField] private Transform playerCamera;

    private float timeSinceLastShot;

    private void Start() {
        shootingAudio.volume = 0.5f;
        reloadingAudio.volume = 0.5f;

        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;
    }

    private void OnDisable() => gunData.reloading = false;

    public void StartReload() {
        if (!gunData.reloading && gunData.currentAmmo != gunData.magazineSize && this.gameObject.activeSelf ) {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload() {
        gunData.reloading = true;

        // Play the audio clip
        if (this.gameObject.activeSelf) {
            reloadingAudio.Play();
        }

        // Set the status text
        statusDisplay.SetText("Reloading");

        yield return new WaitForSeconds(gunData.reloadTime);
        
        // Remove the status text
        statusDisplay.SetText("");

        gunData.currentAmmo = gunData.magazineSize;
        gunData.reloading = false;
    }

    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);

    private void Shoot() {
        Vector3 direction = GetDirection();
        if (gunData.currentAmmo > 0) {
            if (CanShoot()) {
                if (Physics.Raycast(playerCamera.position, direction, out RaycastHit hit, gunData.maxDistance)) {
                    IDamageable damageable = hit.transform.GetComponent<IDamageable>();
                    damageable?.TakeDamage(gunData.damage);

                    if (hit.collider.CompareTag("Enemy")) {
                        hitSoundSource.PlayOneShot(hitSound, 0.5f);
                    }

                    // Render the bullet decal (if it does not hit an enemy)
                    if (!hit.collider.CompareTag("Enemy")) {
                        Instantiate(bulletDecal, hit.point, Quaternion.LookRotation(hit.normal));
                    }

                    
                }
                HandleGunInfo();

                // Render the bullet trail
                if (this.gameObject.activeSelf) {

                    // TODO: Bad performance. Replace with object pooling.
                    TrailRenderer trail = Instantiate(bulletTrail, bulletSpawnPoint.position, Quaternion.identity);
                    StartCoroutine(SpawnTrail(trail, hit));
                }
            }
        }
    }

    private Vector3 GetDirection() {
        Vector3 direction = transform.forward;
        direction += new Vector3(
            Random.Range(-spread.x, spread.x),
            Random.Range(-spread.y, spread.y),
            Random.Range(-spread.z, spread.z)
        );
        direction.Normalize();
        return direction;
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit hit) {
        float time = 0;
        Vector3 startPosition = trail.transform.position;

        while (time < 1) {
            trail.transform.position = Vector3.Lerp(startPosition, hit.point, time);
            time += Time.deltaTime / hit.distance * 100f;

            yield return null;
        }

        trail.transform.position = hit.point;
        Destroy(trail.gameObject, trail.time);
    }

    private void HandleGunInfo() {
        gunData.currentAmmo--;
        timeSinceLastShot = 0;

        // Create the muzzle flash
        GameObject Flash = Instantiate(muzzleFlash, muzzle);
        Destroy(Flash, 0.1f);

        // Play the audio clip
        if (this.gameObject.activeSelf) {
            shootingAudio.Play();
        }
    }

    private void Update() {
        timeSinceLastShot += Time.deltaTime;

        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward);

        if (ammunitionDisplay != null) {
            ammunitionDisplay.SetText(gunData.currentAmmo + " / " + gunData.magazineSize);
        }

        if (!gunData.reloading && gunData.currentAmmo == 0) {
            StartCoroutine(Reload());
        }
    }
}

// Code modified from: https://www.youtube.com/watch?v=kXbQMhwj5Uc
