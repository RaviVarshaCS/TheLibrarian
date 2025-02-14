using UnityEngine;
using UnityEngine.UI;  // Required for button interaction
using System.Collections;

/*
To-Do: Split the buttons -> one for moving book to center of desk, one for opening Reading Panel.
Book Component may be redundant -> check if needed, if not, remove
*/

public class Shelving : MonoBehaviour
{

    public Transform centerPosition; 
    public float moveSpeed = 5f;
    public GameObject readableViewUI; 
    public int bookOrder; 
    public Button publicButton; 
    public Button restrictedButton; 
    
    
    public GameObject[] books;

    private bool isInReadableView = false;
    private bool isAtCenter = false; // Whether the book is at the center
    private Vector3 originalPosition;
    private float interactionCooldown = 0.2f; // Cooldown time in seconds
    private float lastInteractionTime = 0f;
    public List<bookData> currDayData;
    public Button openBook;
    public int currentBook = 1;


    private void Start()
    {
        // manage shelving books
        for (int i = 0; i < books.Length; i++)
        {
            BookComponent bookComponent = books[i].GetComponent<BookComponent>();
            if (bookComponent == null)
            {
                Debug.LogError($"Book {books[i].name} is missing a BookComponent!");
                continue;
            }
            
        }

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
        int runningDay = GameManager.currDay();
        currDayData = BookDatabase.GetShelvingBooksForDay(runningDay);

        // set data for each game object
        for(int i = 0; i < currDayData.Length(); i++) {
            books[i].GetComponent<BookComponent>().SetBookData(currDayData[i]);
        }
    }


    public void OnOpenBookClick()
    {   
        if(currentBook <= 3) {
            readableViewUI.GetComponentInChildren<Book>().SetBookPages(currDataData[currentBook].sprites);
        } else {
            return;
        }
        
        // Interaction cooldown to prevent spamming errors
        if (Time.time - lastInteractionTime < interactionCooldown) {
            return;
        }
        lastInteractionTime = Time.time;

        if(!isAtCenter) { // Move book to center
            MoveToCenter();
        } else if (!isInReadableView) { // On second click, open reading panel
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
        while (timeElapsed < 1f)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, timeElapsed);
            timeElapsed += Time.deltaTime * moveSpeed;
            yield return null;
        }

        transform.position = targetPosition; // Ensure it reaches exactly
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
        // PlayerReputation.Instance.UpdateReputation(bookDataSO.reputationScore);

        GameManager.Instance.ModifyReputation(currDataData[currentBook].publicReputation);
        RemoveBookFromDesk();
        CloseReadableView();  
        currentBook++; // update to the next book

        if(CurrentBook > 3) {
            CompleteTask("Shelving"); // from here, moving to next scene is handled by HUD
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

        if(CurrentBook > 3) {
            CompleteTask("Shelving");
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
