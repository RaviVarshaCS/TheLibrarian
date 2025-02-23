// using UnityEngine;

// public class ColliderManager : MonoBehaviour
// {
//     public GameObject currentOccupant; // Tracks the book occupying the center

//     // Checks if the center is currently occupied
//     public bool isColliding
//     {
//         get { return currentOccupant != null; }
//     }

//     private void OnTriggerEnter(Collider other)
//     {
//         // Check if the object entering is a book
//         BookInteraction book = other.GetComponent<BookInteraction>();
//         if (book != null)
//         {
//             if (currentOccupant == null)
//             {
//                 // Allow the book to occupy the center
//                 currentOccupant = book.gameObject; // Track the book
//                 Debug.Log("Book moved to center.");
//             }
//             else
//             {
//                 // Log that the center is already occupied
//                 Debug.Log("Center position is already occupied.");
//             }
//         }
//     }

//     private void OnTriggerExit(Collider other)
//     {
//         // Check if a book is leaving the center
//         BookInteraction book = other.GetComponent<BookInteraction>();
//         if (book != null && currentOccupant == book.gameObject)
//         {
//             // Clear the center occupant if this book is leaving
//             currentOccupant = null;
//             Debug.Log("Book left the center.");
//         }
//     }
// }
