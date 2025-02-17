// using UnityEngine;
// using UnityEngine.UI;  // Required for button interaction
// using System.Collections;

// /*
// To-Do: Book Component may be redundant -> check if needed, if not, remove
// */

// public class Shelving : MonoBehaviour
// {

//     public Transform centerPosition; 
//     public float moveSpeed = 5f;
//     public GameObject readableViewUI; 
//     public int bookOrder; 
//     public Button giveButton; 
//     public Button restrictButton; 
    
    
//     public GameObject[] books;

//     private bool isInReadableView = false;
//     private bool isAtCenter = false; // Whether the book is at the center
//     private Vector3 originalPosition;
//     private float interactionCooldown = 0.2f; // Cooldown time in seconds
//     private float lastInteractionTime = 0f;
//     public List<bookData> currDayData;
//     public Button openBook;
//     public int currentBook;


//     private void Start()
//     {
//         currentBook = 0;

//         originalPosition = transform.position;
//         if (readableViewUI != null)
//         {
//             readableViewUI.SetActive(false);
//         }

//         // Set up button listeners
//         if (giveButton != null)
//         {
//             giveButton.onClick.AddListener(OnGiveButtonClick);
//         }

//         if (restrictButton != null)
//         {
//             restrictButton.onClick.AddListener(OnRestrictButtonClick);
//         }

//         if (openBook != null) 
//         {
//             openBook.onClick.AddListener(OnOpenBookClick);
            
//         }


//         // set up all the data for the books, updates on each load of the scene
//         getAllDayContents();


//     }

//     public void getAllDayContents() {
//         // get current shelving data
//         int runningDay = GameManager.Instance.currDay();
//         currDayData = BookDatabase.GetTalkingBooksForDay(runningDay);

//         // set data for each game object
//         for(int i = 0; i < currDayData.Length(); i++) {
//             books[i].GetComponent<BookComponent>().SetBookData(currDayData[i]);
//         }
//     }


//     public void OnOpenBookClick()
//     {   
//         if(TalkingManager.Instance.isFirstTalk() == true) {
//             currentBook = 0;
//             readableViewUI.GetComponentInChildren<Book>().SetBookPages(currDataData[currentBook].sprites);
//         } else {
//             currentBook = 1;
//             readableViewUI.GetComponentInChildren<Book>().SetBookPages(currDataData[currentBook].sprites);
//         }

//         if (!isInReadableView) { // On second click, open reading panel
//             OpenReadableView();
//         }  else {
//             return;
//         }
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
//     private void OnGiveButtonClick()
//     {
//         Debug.Log("Give Button Clicked!");
//         // PlayerReputation.Instance.UpdateReputation(bookDataSO.reputationScore);

//         GameManager.Instance.ModifyReputation(currDataData[currentBook].publicReputation);
//         GameManager.Instance.ModifyRelationship(currDataData[currentBook].publicRelationship);
//         RemoveBookFromDesk();
//         CloseReadableView();  


//         if(currentBook >= 1) {
//             CompleteTask("Talking");
//         } else {
//             TalkingManager.Instance.finishedFirstTalk();
//         }
//     }

//     // Method for handling the restricted button click
//     private void OnRestrictButtonClick()
//     {
//         Debug.Log("Don't Give Button Clicked!");
//         GameManager.Instance.ModifyReputation(currDayData[currentBook].restrictedReputation);
//         GameManager.Instance.ModifyRelationship(currDayData[currentBook].restrictedRelationship);

//         RemoveBookFromDesk();
//         CloseReadableView(); 


//         if(currentBook >= 1) {
//             CompleteTask("Talking");
//         } else {
//             TalkingManager.Instance.finishedFirstTalk();
//         }
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
