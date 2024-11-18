using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 120f;
    
    [Header("Camera Settings")]
    [SerializeField] private Vector3 cameraOffset = new Vector3(0f, 2f, -5f);
    [SerializeField] private float cameraSmoothSpeed = 5f;
    
    [Header("Physics Settings")]
    [SerializeField] private float gravity = -9.81f;
    
    private CharacterController characterController;
    private Transform cameraTransform;
    private Vector3 playerVelocity;
    private float currentRotation;
    private Vector3 currentCameraPosition;
    private bool isFirstFrame = true;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        
        // Initialize rotation to match current Y rotation
        currentRotation = transform.eulerAngles.y;
        
        // Initialize camera position
        currentCameraPosition = transform.position + transform.rotation * cameraOffset;
        cameraTransform.position = currentCameraPosition;
    }

    private void Update()
    {
        if (isFirstFrame)
        {
            isFirstFrame = false;
            return; // Skip first frame to ensure proper initialization
        }

        HandleMovement();
        ApplyGravity();
    }

    private void LateUpdate()
    {
        UpdateCamera();
    }

    private void HandleMovement()
    {
        // Get input values
        float verticalInput = Input.GetAxis("Vertical");    // Up/Down arrows or W/S
        float horizontalInput = Input.GetAxis("Horizontal"); // Left/Right arrows or A/D
        
        // Handle rotation with smoothing
        if (horizontalInput != 0)
        {
            float rotationDelta = horizontalInput * rotationSpeed * Time.deltaTime;
            currentRotation += rotationDelta;
            transform.rotation = Quaternion.Euler(0f, currentRotation, 0f);
        }
        
        // Handle forward/backward movement
        if (verticalInput != 0)
        {
            Vector3 moveDirection = transform.forward * verticalInput;
            characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
        }
    }

    private void ApplyGravity()
    {
        if (characterController.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f; // Small downward force to keep grounded
        }
        else
        {
            playerVelocity.y += gravity * Time.deltaTime;
        }

        characterController.Move(playerVelocity * Time.deltaTime);
    }

    private void UpdateCamera()
    {
        // Calculate the desired camera position based on player's position and rotation
        Vector3 desiredPosition = transform.position + transform.rotation * cameraOffset;
        
        // Smoothly move the camera
        currentCameraPosition = Vector3.Lerp(currentCameraPosition, desiredPosition, cameraSmoothSpeed * Time.deltaTime);
        
        // Update camera position and make it look at the player
        cameraTransform.position = currentCameraPosition;
        
        // Smoothly rotate the camera
        Vector3 lookDirection = (transform.position + Vector3.up) - cameraTransform.position;
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
        cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, targetRotation, cameraSmoothSpeed * Time.deltaTime);
    }
}