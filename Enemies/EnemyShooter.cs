using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour {
    [Header("General")]
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private Transform gunPoint;
    [SerializeField] private LayerMask layerMask;

    [Header("Gun")]
    [SerializeField] private Vector3 spread = new Vector3(0.06f, 0.06f, 0.06f);
    [SerializeField] private TrailRenderer bulletTrail;
    [SerializeField] private int maxAmmo = 30;

    private EnemyReferences enemyReferences;
    private int currentAmmo;

    private void Awake() {
        enemyReferences = GetComponent<EnemyReferences>();
        currentAmmo = maxAmmo;
    }

    public void Shoot() {
        if (ShouldReload()) return;

        Vector3 direction = GetDirection();
        if (Physics.Raycast(shootingPoint.position, direction, out RaycastHit hit, float.MaxValue, layerMask)) {
            Debug.DrawLine(shootingPoint.position, shootingPoint.position + direction * 10f, Color.red, 1f);

            // TODO: Bad performance. Replace with object pooling.
            TrailRenderer trail = Instantiate(bulletTrail, gunPoint.position, Quaternion.identity);
            StartCoroutine(SpawnTrail(trail, hit));

            currentAmmo--;
        }
    }

    public bool ShouldReload() {
        return currentAmmo <= 0;
    }

    public void Reload() {
        currentAmmo = maxAmmo;
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
        float time = 0f;
        Vector3 startPosition = trail.transform.position;

        while (time < 1f) {
            trail.transform.position = Vector3.Lerp(startPosition, hit.point, time);
            time += Time.deltaTime / trail.time;

            yield return null;
        }

        trail.transform.position = hit.point;
        Destroy(trail.gameObject, trail.time);
    }

}
