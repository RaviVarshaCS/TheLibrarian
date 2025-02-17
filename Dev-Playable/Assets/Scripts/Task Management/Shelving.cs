using UnityEngine;
using UnityEngine.UI;  // Required for button interaction
using System.Collections;
using System.Collections.Generic;

/*
To-Do: Split the buttons -> one for moving book to center of desk, one for opening Reading Panel.
Book Component may be redundant -> check if needed, if not, remove
*/

public class Shelving : MonoBehaviour
{

    public Transform centerPosition; 
    public float moveSpeed = 5f;
    public GameObject readableViewUI; 
    public Button publicButton; 
    public Button restrictedButton; 
    
    
    public GameObject[] books;

    private bool isInReadableView = false;
    private bool isAtCenter = false; // Whether the book is at the center
    private Vector3 originalPosition;
    private float interactionCooldown = 0.2f; // Cooldown time in seconds
    private float lastInteractionTime = 0f;
    public List<BookData> currDayData;
    public Button openBook;
    public int currentBook = 0;


    private void Start()
    {

        originalPosition = transform.position;
        if (readableViewUI != null)
        {
            readableViewUI.SetActive(false);
        }

        // Set up button listeners
        if (publicButton != null)
        {
            publicButton.onClick.AddListener(OnPublicButtonClick);
        }

        if (restrictedButton != null)
        {
            restrictedButton.onClick.AddListener(OnRestrictedButtonClick);
        }

        if (openBook != null) 
        {
            openBook.onClick.AddListener(OnOpenBookClick);
            
        }

        // set up all the data for the books, updates on each load of the scene
        getAllDayContents();


    }

    public void getAllDayContents() {
        // get current shelving data
        int runningDay = GameManager.Instance.currDay();
        currDayData = BookDatabase.Instance.GetShelvingBooksForDay(runningDay);
        Debug.Log("All Day contents received");

    }


    public void OnOpenBookClick()
    { 
        Debug.Log("Open book button clicked!");
        if(currentBook <= 2) {
            readableViewUI.GetComponentInChildren<Book>().SetBookPages(currDayData[currentBook].sprites);
            Debug.Log("Fetched next book -> " + currDayData[currentBook].bookName);
        } else {
            return;
        }
        
        // Interaction cooldown to prevent spamming errors
        if (Time.time - lastInteractionTime < interactionCooldown) {
            return;
        }
        lastInteractionTime = Time.time;

        if (!isInReadableView) { // On second click, open reading panel
            OpenReadableView();
        }  else {
            return;
        }
    }

    private void MoveToCenter()
    {
        // Move the book to the center position
        StartCoroutine(MoveToCenterCoroutine());
    }

    private IEnumerator MoveToCenterCoroutine()
    {
        float timeElapsed = 0f;
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = centerPosition.position;

        // Move the book to the center position
        while (timeElapsed < 1f && currentBook <= 2)
        {
            // Smooth movement using Lerp
            books[currentBook].transform.position = Vector3.Lerp(startPosition, targetPosition, timeElapsed);
            timeElapsed += Time.deltaTime * moveSpeed;  // Increase time for smooth movement
            yield return null;
        }

        // Ensure it reaches exactly the target position
        books[currentBook].transform.position = targetPosition;
        isAtCenter = true;
    }

    private void OpenReadableView()
    {
        isInReadableView = true;

        if (readableViewUI != null)
        {
            readableViewUI.SetActive(true); // Display UI or open readable view
        }
        Debug.Log("Opened readable view.");
    }

    private void CloseReadableView()
    {
        isInReadableView = false;

        if (readableViewUI != null)
        {
            readableViewUI.SetActive(false); // Close UI or return to normal
        }
        Debug.Log("Closed readable view.");
    }

    // Method for handling the public button click
    private void OnPublicButtonClick()
    {
        Debug.Log("Public Button Clicked!");
        GameManager.Instance.ModifyReputation(currDayData[currentBook].publicReputation);
        RemoveBookFromDesk();
        CloseReadableView();  
        currentBook++; // update to the next book

        if(currentBook > 2) {
            GameManager.Instance.CompleteTask("Shelving"); // from here, moving to next scene is handled by HUD
        }
    }

    // Method for handling the restricted button click
    private void OnRestrictedButtonClick()
    {
        Debug.Log("Restricted Button Clicked!");
        GameManager.Instance.ModifyReputation(currDayData[currentBook].restrictedReputation);
        RemoveBookFromDesk();
        CloseReadableView(); 
        currentBook++; // update to the next book

        if(currentBook > 2) {
            GameManager.Instance.CompleteTask("Shelving");
        }
    }

    // Method to remove the book from the desk (either hide it or move it off the desk)
    private void RemoveBookFromDesk()
    {
        if (isAtCenter) // Only move the book that is at the center
        {
            Debug.Log("Book removed from desk.");
            // Move the book off the screen or disable it
            transform.position = new Vector3(1000, 1000, 1000); // Move it off-screen
            gameObject.SetActive(false); // Alternatively, hide the book (disable rendering)
            isAtCenter = false; // Reset center status
            Debug.Log("Book removed from desk.");
        }

    }

    public void ResetBookPosition()
    {
        transform.position = originalPosition;
        isAtCenter = false; // Reset the center status
        gameObject.SetActive(true); // Make sure the book is active again
    }
}
