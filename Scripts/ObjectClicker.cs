using UnityEngine;

public class ObjectClicker : MonoBehaviour
{
    public Camera mainCamera; // Reference to your fixed camera

    void Update()
    {
        // Check for mouse click
        if (Input.GetMouseButtonDown(0)) // 0 is the left mouse button
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition); // Ray from camera to mouse click
            RaycastHit hit;

            // Check if the ray hits an object with a collider
            if (Physics.Raycast(ray, out hit))
            {
                // Log the name of the clicked object
                Debug.Log("Clicked on: " + hit.collider.gameObject.name);

                // Example: Call a method on the clicked object (e.g., to open a book)
                BookInteraction book = hit.collider.gameObject.GetComponent<BookInteraction>();
                if (book != null)
                {
                    book.OnMouseDown(); // Custom method in your BookInteraction script
                }
            }
        }
    }
}
