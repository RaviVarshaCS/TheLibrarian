using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public Transform cameraTransform;
    public Vector3 cameraOffset = new Vector3(0, 2, -4);
    
    void Update()
    {
        HandleMovement();
        UpdateCameraPosition();
    }

    void HandleMovement()
    {
        float moveInput = Input.GetAxis("Vertical");
        float rotationInput = Input.GetAxis("Horizontal");
        
        transform.Translate(Vector3.forward * moveInput * moveSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up * rotationInput * rotationSpeed * Time.deltaTime);
    }

    void UpdateCameraPosition()
    {
        if (cameraTransform != null)
        {
            Vector3 desiredPosition = transform.position + transform.rotation * cameraOffset;
            RaycastHit hit;

            // Raycast from player to desired camera position
            if (Physics.Linecast(transform.position, desiredPosition, out hit))
            {
                // If collision detected, move camera closer
                cameraTransform.position = hit.point + hit.normal * 0.2f; // Small offset to prevent clipping
            }
            else
            {
                cameraTransform.position = desiredPosition;
            }

            cameraTransform.LookAt(transform.position + Vector3.up * 1.5f);
        }
    }
}
