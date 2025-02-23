using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 100f;
    // private CharacterController controller;
    
    void Start()
    {
        // controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Incremental turning
        float horizontal = Input.GetAxis("Horizontal"); // Left/Right keys
        float vertical = Input.GetAxis("Vertical"); // Forward/Backward keys

        // Rotate character incrementally
        transform.Rotate(0, horizontal * turnSpeed * Time.deltaTime, 0);

        // Move character forward in its facing direction
        Vector3 forward = transform.forward * vertical * moveSpeed * Time.deltaTime;
        // if (controller.isGrounded)
        //     {
        //         controller.Move(forward);
        //     }
    }
}