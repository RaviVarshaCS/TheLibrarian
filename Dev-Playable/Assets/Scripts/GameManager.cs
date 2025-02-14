using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    // Game state variables
    public int reputation = 50;          // Initial reputation
    public int relationship = 50;        // Initial relationship
    public int currentDay = 0;           // Current day in the game (0-3)
    private int totalDays = 3;           // Total number of days
    public List<BookData> booksForToday; // Books for the current day

    // Task completion tracking
    public bool shelvingCompleted = false;
    public bool patronInteractionCompleted = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        LoadScene(MainMenu);
    }

    // Scene Transitions
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void StartGame()
    {
        currentDay = 1;
        LoadBooksForToday(); 
        LoadScene("MainLibrary");
    }

    public void ProceedToNextDay()
    {
        if (currentDay < totalDays)
        {
            currentDay++;
            LoadBooksForToday();  
            ResetDailyTasks();
            LoadScene("MainLibraryScene");
        }
        else
        {
            CheckGameEnd();
        }
    }

    public int currDay() 
    {
        return currentDay;

    }

    // Load books for the current day
    private void LoadBooksForToday()
    {
        booksForToday = BookDatabase.Instance.GetBooksForDay(currentDay);
    }


    // Task Completion Check
    public void CompleteTask(string task)
    {
        if (task == "Shelving") shelvingCompleted = true;
        else if (task == "Patron") patronInteractionCompleted = true;

        CheckDayCompletion();
    }

    public void CheckDayCompletion()
    {
        // Check if all tasks for the day are completed
        if (shelvingCompleted && patronInteractionCompleted)
        {
            // All tasks done, proceed to next day
            Invoke("ProceedToNextDay", 2f);
        }
    }

    // Reset daily task states
    public void ResetDailyTasks()
    {
        shelvingCompleted = false;
        patronInteractionCompleted = false;
    }

    // Update Reputation and Relationship values
    public void ModifyReputation(int amount)
    {
        reputation += amount;
        CheckGameOver(); // Check if game should end due to low reputation
    }

    public void ModifyRelationship(int amount)
    {
        relationship += amount;
        CheckGameOver(); // Check if game should end due to low relationship
    }

    // Check if the game should end based on reputation and relationship
    private void CheckGameOver()
    {
        if (reputation < 30 || relationship < 30)
        {
            LoadFailureScene();
        }
    }

    // Game end: Success or Failure
    private void CheckGameEnd()
    {
        if (reputation >= 30 && relationship >= 30)
        {
            LoadSuccessScene();
        }
        else
        {
            LoadFailureScene();
        }
    }

    private void LoadFailureScene()
    {
        LoadScene("FailureScene");
    }

    private void LoadSuccessScene()
    {
        LoadScene("SuccessScene");
    }

    // Start the game (called from main menu)
    public void LoadMainMenu()
    {
        LoadScene("MainMenuScene");
    }
}
