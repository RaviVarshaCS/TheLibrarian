using UnityEngine;

public class ObjectClicker : MonoBehaviour
{
    public Camera mainCamera; // Reference to your fixed camera
    // public int bookOrder = 1; // Order of the book on the desk


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
                GameObject clickedObject = hit.collider.gameObject;

                // Log the name of the clicked object
                Debug.Log("Clicked on: " + clickedObject.name);



                    // Example: Call a method on the clicked object (e.g., to open a book)
                    BookInteraction book = clickedObject.GetComponent<BookInteraction>();
                    if (book != null)
                    {
                        // Debug.Log("Book order: " + book.bookOrder + ", Expected order: " + bookOrder);

                        book.InteractWithBook(/*bookOrder*/);
                    }
            }
        }
    }
}
