// using UnityEngine;
// using UnityEngine.UI;  // Required for button interaction
// using System.Collections;

// public class BookInteraction : MonoBehaviour
// {
//     [SerializeField] private BookDataSO bookDataSO;

//     public Transform centerPosition; // Position in the center
//     public float moveSpeed = 5f;
//     public GameObject readableViewUI; // UI or object for readable view
//     public int bookOrder; // Order of the book on the desk
//     public Button publicButton; // Reference to public button
//     public Button restrictedButton; // Reference to restricted button

//     private bool isInReadableView = false;
//     private bool isAtCenter = false; // Whether the book is at the center
//     private Vector3 originalPosition;
//     private float interactionCooldown = 0.2f; // Cooldown time in seconds
//     private float lastInteractionTime = 0f;

//     private void Start()
//     {
//         originalPosition = transform.position; // Save the original position
//         if (readableViewUI != null)
//         {
//             readableViewUI.SetActive(false); // Ensure readable view UI is off initially
//         }

//         // Set up button listeners
//         if (publicButton != null)
//         {
//             publicButton.onClick.AddListener(OnPublicButtonClick);
//         }

//         if (restrictedButton != null)
//         {
//             restrictedButton.onClick.AddListener(OnRestrictedButtonClick);
//         }
//     }

//     public void SetBookData(BookDataSO bookData)
//     {
//         bookDataSO = bookData;
//     }

//     public void InteractWithBook()
//     {
//         // if (bookOrder == expectedOrder)
//         // {
//             // Book clicked in the correct order
//             OnCorrectOrderClick();
//     }

//     private void OnCorrectOrderClick()
//     {
//         //TODO: probably should check if data loaded??


//         readableViewUI.GetComponentInChildren<Book>().SetBookPages(bookDataSO.bookPages);

//         // Prevent interaction if cooldown hasn't passed
//         if (Time.time - lastInteractionTime < interactionCooldown)
//         {
//             return;
//         }

//         lastInteractionTime = Time.time; // Update last interaction time

//         // If the book isn't at the center, move it there.
//         if (!isAtCenter)
//         {
//             MoveToCenter();
//         }
//         else if (!isInReadableView)
//         {
//             OpenReadableView();
//         }
//         else
//         {
//             CloseReadableView();
//         }
//     }

//     private void MoveToCenter()
//     {
//         // Move the book to the center position
//         StartCoroutine(MoveToCenterCoroutine());
//     }

//     private IEnumerator MoveToCenterCoroutine()
//     {
//         float timeElapsed = 0f;
//         Vector3 startPosition = transform.position;
//         Vector3 targetPosition = centerPosition.position;

//         // Move the book to the center position
//         while (timeElapsed < 1f)
//         {
//             transform.position = Vector3.Lerp(startPosition, targetPosition, timeElapsed);
//             timeElapsed += Time.deltaTime * moveSpeed;
//             yield return null;
//         }

//         transform.position = targetPosition; // Ensure it reaches exactly
//         isAtCenter = true;
//     }

//     private void OpenReadableView()
//     {
//         isInReadableView = true;

//         if (readableViewUI != null)
//         {
//             readableViewUI.SetActive(true); // Display UI or open readable view
//         }
//         Debug.Log("Opened readable view.");
//     }

//     private void CloseReadableView()
//     {
//         isInReadableView = false;

//         if (readableViewUI != null)
//         {
//             readableViewUI.SetActive(false); // Close UI or return to normal
//         }
//         Debug.Log("Closed readable view.");
//     }

//     // Method for handling the public button click
//     private void OnPublicButtonClick()
//     {
//         Debug.Log("Public Button Clicked!");
//         PlayerReputation.Instance.UpdateReputation(bookDataSO.reputationScore);
//         RemoveBookFromDesk();
//         CloseReadableView();  // Close the readable view when a button is clicked
//     }

//     // Method for handling the restricted button click
//     private void OnRestrictedButtonClick()
//     {
//         Debug.Log("Restricted Button Clicked!");
//         int reputationValue = bookDataSO.reputationScore * -1;
//         PlayerReputation.Instance.UpdateReputation(reputationValue);
//         RemoveBookFromDesk();
//         CloseReadableView();  // Close the readable view when a button is clicked
//     }

//     // Method to remove the book from the desk (either hide it or move it off the desk)
//     private void RemoveBookFromDesk()
//     {
//         if (isAtCenter) // Only move the book that is at the center
//         {
//             Debug.Log("Book removed from desk.");
//             // Move the book off the screen or disable it
//             transform.position = new Vector3(1000, 1000, 1000); // Move it off-screen
//             gameObject.SetActive(false); // Alternatively, hide the book (disable rendering)
//             isAtCenter = false; // Reset center status
//             Debug.Log("Book removed from desk.");
//         }
//     }

//     public void ResetBookPosition()
//     {
//         transform.position = originalPosition;
//         isAtCenter = false; // Reset the center status
//         gameObject.SetActive(true); // Make sure the book is active again
//     }
// }
