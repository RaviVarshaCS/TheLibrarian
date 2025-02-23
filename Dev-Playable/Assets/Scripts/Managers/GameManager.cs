using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI; 

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    // Game state variables
    public int reputation = 50;          // Initial reputation
    public int relationship = 50;        // Initial relationship
    public int currentDay = 0;           // Current day in the game (0-3)
    private int totalDays = 2;           // Total number of days
    public List<BookData> booksForToday; // Books for the current day

    // Task completion tracking
    public bool shelvingCompleted = false;
    public bool patronInteractionCompleted = false;
    public bool dayComplete = false;

    public Image fadeImage;
    public float fadeDuration = 1f;


    void Awake()
    {
        Debug.Log("GameManager is Running");
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

    // void Start()
    // {
        

    //     // LoadScene("Main Menu");
    // }

    // Scene Transitions
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void StartGame()
    {
        currentDay = 1;
        LoadBooksForToday(); 
        LoadScene("Library");
    }

    public void ProceedToNextDay()
    {
        if (currentDay < totalDays)
        {
            currentDay++;
            dayComplete = false;
            LoadBooksForToday();  
            ResetDailyTasks();
            // LoadScene("Library");
            FadeToScene("Library");
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
            // Invoke("ProceedToNextDay", 2f);
            dayComplete = true;
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
        if (reputation >= 20 && relationship >= 20)
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
        FadeToScene("Failure");
    }

    private void LoadSuccessScene()
    {
        FadeToScene("Success");
    }

    // Start the game (called from main menu)
    public void LoadMainMenu()
    {
        LoadScene("Library");
    }

    public void FadeToScene(string sceneName)
    {
        // if (fadeImage == null)
        // {
        //     Debug.LogError("fadeImage is not assigned!");
        //     return;
        // }

        fadeImage.gameObject.SetActive(true);
        fadeImage.color = new Color(0, 0, 0, 0); // Start as transparent
        StartCoroutine(FadeOutIn(sceneName));
    }

    private IEnumerator FadeOutIn(string sceneName)
    {
        // Fade out
        yield return StartCoroutine(Fade(1f));
        
        // Load the next scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        
        // Wait until the scene is fully loaded
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Fade in
        yield return StartCoroutine(Fade(0f));
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float currentAlpha = fadeImage.color.a;
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(currentAlpha, targetAlpha, timeElapsed / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, targetAlpha); // Ensure the final alpha is exact
        fadeImage.gameObject.SetActive(false);
    }

    public void resetGame() 
    {
        reputation = 50;
        relationship = 50;
        currentDay = 0;

        shelvingCompleted = false;
        patronInteractionCompleted = false;
        dayComplete = false;

        FadeToScene("Main Menu");
    }
}
