using System.Net.NetworkInformation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [Header("Settings")]
    public float playerHeight;
    private Rigidbody playerBody;

    [Header("Movement")]
    public float movementSpeed;
    public float movementMultiplier;
    [SerializeField] public float airMultiplier;

    private Vector3 movementDirection;
    private float horizontalMovement;
    private float verticalMovement;

    [Header("Sprinting")]
    [SerializeField] public float walkingSpeed;
    [SerializeField] public float sprintingSpeed;
    [SerializeField] public float acceleration;

    [Header("Jumping")]
    public float jumpForce;

    [Header("Drag")]
    public float groundDrag;
    public float airDrag;

    [Header("Ground Detection")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundDistance;
    private bool isGrounded;
    
    [Header("Slope Detection")]
    RaycastHit slopeHit;
    private bool OnSlope() {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f)) {
            if (slopeHit.normal != Vector3.up) {
                return true;
            }
        }
        return false;
    }
    private Vector3 slopeMoveDirection;

    [Header("State")]
    public MovementState movementState;

    public enum MovementState {
        Idle,
        Walking,
        Sprinting,
        Air,
    }

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;

    [Header("References")]
    [SerializeField] public Transform orientation;
    
    private void Start() {
        playerBody = GetComponent<Rigidbody>();
        playerBody.freezeRotation = true;
    }

    private void Update() {
        // Checks if the player is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    
        HandleInput();
        HandleState();
        HandleDrag();

        ControlSpeed();

        if (Input.GetKeyDown(jumpKey) && isGrounded) {
            Jump();
        }

        // Checks if the player is on a slope
        slopeMoveDirection = Vector3.ProjectOnPlane(movementDirection, slopeHit.normal);
    }

    private void FixedUpdate() {
        MovePlayer();
    }

    private void HandleInput() {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        movementDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
    }

    private void HandleDrag() {
        if (isGrounded) {
            playerBody.drag = groundDrag;
        } else if (!isGrounded) {
            playerBody.drag = airDrag;
        }
    }

    private void HandleState() {
        // Sprinting
        if (isGrounded && Input.GetKey(sprintKey)) {
            movementState = MovementState.Sprinting;
        }

        // Walking
        else if (isGrounded && gameObject.GetComponent<Rigidbody>().velocity.magnitude > 1) {
            movementState = MovementState.Walking;
        }

        // Idle
        else if (isGrounded) {
            movementState = MovementState.Idle;
        }

        // Jumping / Air
        else {
            movementState = MovementState.Air;
        }
    }

    private void ControlSpeed() {
        if (Input.GetKey(sprintKey) && isGrounded) {
            movementSpeed = Mathf.Lerp(movementSpeed, sprintingSpeed, acceleration * Time.deltaTime);
        } else {
            movementSpeed = Mathf.Lerp(movementSpeed, walkingSpeed, acceleration * Time.deltaTime);
        }
    }

    private void MovePlayer() {
        if (isGrounded && !OnSlope()) {
            playerBody.AddForce(movementDirection.normalized * movementSpeed * movementMultiplier, ForceMode.Acceleration);
        } else if (isGrounded && OnSlope()) {
            playerBody.AddForce(slopeMoveDirection.normalized * movementSpeed * movementMultiplier, ForceMode.Acceleration);
        } else if (!isGrounded) {
            playerBody.AddForce(movementDirection.normalized * movementSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }
    }

    private void Jump() {
        if (isGrounded) {
            playerBody.velocity = new Vector3(playerBody.velocity.x, 0, playerBody.velocity.z);
            playerBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }
}

// Code modified from: https://www.youtube.com/watch?v=LqnPeqoJRFY
