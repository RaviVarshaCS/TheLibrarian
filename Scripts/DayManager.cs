using System.Threading.Tasks;
using UnityEngine;

public enum DayState
{
    Shelving,
    Loaning,
    Exploration,
    EOD // End of day stats screen
}

public class DayManager : MonoBehaviour, IDataPersistance
{
    public static DayManager Instance { get; private set; }
    public DayState CurrentState;

    [SerializeField] public DayDataSO[] allDays;
    private DayDataSO dayDataSO;

    private void Awake()
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

    public void HandleGameStateChanged(Component sender, object data)
    {
        if (data is GameState)
        {
            GameState newState = (GameState)data;

            // TEMP need to change to add in day states
            // currently equating library workday to shelving
            if (newState == GameState.LibraryWorkday) {
                StartDay();
            }
        }
    }

    private void StartDay()
    {
        UpdateDayState(DayState.Shelving);
        LoadDayData(GameManager.Instance.CurrentDay);
    }

    private void LoadDayData(int currentDay)
    {
        if (currentDay < 0 || currentDay >= allDays.Length)
        {
            Debug.LogError("Day out of range");
            return;
        }

        dayDataSO = allDays[currentDay];
        Debug.Log($"Day {currentDay} data loaded for game state: {CurrentState}");

        // Load data based on the current game state
        switch (CurrentState)
        {
            case DayState.Shelving:
                UpdateShelvingBooksWithDayData();
                break;
            case DayState.Loaning:
                UpdateLoanRequestsWithDayData();
                break;
            case DayState.Exploration:
                // Handle exploration phase if needed
                Debug.Log("Exploration phase started (data not loaded here).");
                break;
            default:
                Debug.LogWarning("Unhandled day state!");
                break;
        }
    }

    private void UpdateShelvingBooksWithDayData()
    {
        if (CurrentState != DayState.Shelving)
        {
            Debug.LogWarning("Not in shelving phase. Skipping book updates.");
            return;
        }

        BookInteraction[] shelvingBooks = FindObjectsOfType<BookInteraction>();

        for (int i = 0; i < 3; i++)
        {
            BookDataSO bookData = dayDataSO.shelvingBooks[i];
            shelvingBooks[i].SetBookData(bookData);
        }

        Debug.Log("Books updated");
    }

    private void UpdateLoanRequestsWithDayData()
    {
        if (CurrentState != DayState.Loaning)
        {
            Debug.LogWarning("Not in loaning phase. Skipping loan request updates.");
            return;
        }

        // LoanRequest[] loanRequests = FindObjectsOfType<LoanRequest>();

        // for(int i = 0; i < 3; i++)
        // {
        //     BookDataSO bookData = dayDataSO.loanRequests[i];
        //     loanRequests[i].SetBookData(bookData);
        // }

        Debug.Log("Loan requests updated");
    }

    public void UpdateDayState(DayState newState)
    {

        switch (newState)
        {
            case DayState.Shelving:
                // Add shelving phase specific logic here (e.g., disable player movement, etc.)
                break;
            case DayState.Loaning:
                // Add loaning phase specific logic here (e.g., disable player movement)
                break;
            case DayState.Exploration:
                // Add exploration phase specific logic
                break;
            case DayState.EOD:
                // Add end of day stats screen logic here
                break;
            default:
                throw new System.Exception("Invalid day state");
        }


        CurrentState = newState;
        Debug.Log($"Day State: {CurrentState}");
    }

    public void LoadData(GameData data)
    {
        UpdateDayState(data.dayState);
        LoadDayData(data.currentDay);
    }

    public void SaveData(ref GameData data)
    {
        data.dayState = this.CurrentState;
    }
}