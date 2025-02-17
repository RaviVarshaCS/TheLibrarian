using UnityEngine;

public class CanvasPositioner : MonoBehaviour
{
    public Camera mainCamera; // Reference to your camera
    public Canvas canvas;     // Reference to your Canvas

    public Vector3 offset = new Vector3(0, 0, 2); // Offset from the camera position (adjust as needed)
    public float scaleFactor = 10f; // Factor to scale the canvas size for better visibility

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // Default to the main camera if no camera is assigned
        }

        if (canvas != null)
        {
            // Set the Canvas render mode to World Space
            canvas.renderMode = RenderMode.WorldSpace;

            // Position the canvas in front of the camera with an offset
            Vector3 cameraPosition = mainCamera.transform.position;
            Vector3 cameraForward = mainCamera.transform.forward;

            // Apply the offset from the camera
            canvas.transform.position = cameraPosition + cameraForward * offset.z;
            canvas.transform.rotation = Quaternion.LookRotation(cameraForward); // Make sure it's facing the camera

            // Scale the Canvas to make it more visible (adjust this value as needed)
            canvas.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
        }
    }
}
