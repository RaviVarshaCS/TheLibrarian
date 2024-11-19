using UnityEngine;

public class CanvasPosition : MonoBehaviour
{
    public Camera mainCamera; // Reference to the main camera
    public Canvas canvas;     // Reference to the Canvas

    public float distanceFromCamera = 5f; // Distance in front of the camera (adjustable)

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // Default to main camera if not set
        }

        if (canvas != null)
        {
            // Set the Canvas render mode to World Space
            canvas.renderMode = RenderMode.WorldSpace;

            // Calculate the width and height of the camera's viewport in world units
            Rect screenRect = new Rect(0, 0, Screen.width, Screen.height);
            Vector3 bottomLeft = mainCamera.ScreenToWorldPoint(new Vector3(screenRect.xMin, screenRect.yMin, distanceFromCamera));
            Vector3 topRight = mainCamera.ScreenToWorldPoint(new Vector3(screenRect.xMax, screenRect.yMax, distanceFromCamera));

            // Adjust the Canvas size to match the screen size in world space
            Vector3 canvasSize = new Vector3(topRight.x - bottomLeft.x, topRight.y - bottomLeft.y, 0f);

            // Apply the canvas size and place the canvas in front of the camera
            canvas.GetComponent<RectTransform>().sizeDelta = new Vector2(canvasSize.x, canvasSize.y);
            canvas.transform.position = mainCamera.transform.position + mainCamera.transform.forward * distanceFromCamera;

            // Rotate the Canvas to face the camera
            canvas.transform.rotation = Quaternion.LookRotation(mainCamera.transform.forward);
        }
    }
}
