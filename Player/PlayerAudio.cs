using System.Linq;
using UnityEngine;

public class PlayerAudio : MonoBehaviour {
    [Header("References")]
    [SerializeField] private AudioSource walkingAudio;

    private PlayerMovement.MovementState movementState;

    private void Start() {
        walkingAudio.volume = 0.5f;
    }

    private void Update() {
        movementState = GetComponent<PlayerMovement>().movementState;
        
        if (movementState == PlayerMovement.MovementState.Walking) {
            walkingAudio.enabled = true;
        } else if (movementState == PlayerMovement.MovementState.Sprinting) {
            walkingAudio.enabled = true;
        } else {
            walkingAudio.enabled = false;
        }
    }
}
