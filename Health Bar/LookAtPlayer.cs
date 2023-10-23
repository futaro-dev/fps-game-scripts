using UnityEngine;

public class LookAtPlayer : MonoBehaviour {
    private void Update() {
        transform.LookAt(Camera.main.transform.position, Vector3.up);
    }
}
