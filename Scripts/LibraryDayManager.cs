using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO add idata persistance
// - delete librarydataloader?
// points math ;(((
public class LibraryDayManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    public static LibraryDayManager Instance;
    // public LibraryBaseState CurrentState;

    [SerializeField] public DayData dayData;
    private Dictionary<int, DayData> _cachedDayData;
    private FileDataHandler<DayData> dataHandler;

    // public LibraryShelvingState ShelvingState = new LibraryShelvingState();
    // public LibraryExplorationState ExplorationState = new LibraryExplorationState();
    private void Start()
    {
        if (GameManager.Instance != null) {
            this.dataHandler = new FileDataHandler<DayData>(Application.persistentDataPath, this.fileName);
        } else {
            Debug.LogError("GameManager instance is missing!");
        }

        this.dayData = LoadDayData(GameManager.Instance.CurrentDay);

        if (dayData == null)
        {
            Debug.LogError($"Failed to load data for day {GameManager.Instance.CurrentDay}");
        }

        // call event to boot up shelving state
        // rn this is just happening from default bootloader

        // CurrentState = ShelvingState;
        // CurrentState.EnterState(this);
    }

    // update gets called every frame
    // doesnt make sense to update game scenes every frame smh
    // void Update()
    // {
    //     CurrentState.UpdateState(this);
    // }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    // public void SwitchState(LibraryBaseState newState) {
    //     CurrentState = newState;
    //     CurrentState.EnterState(this);
    // }

    public DayData LoadDayData(int currentDay) {
        EnsureDayDataIsCached(currentDay);
        return _cachedDayData[currentDay];
    }

    private void EnsureDayDataIsCached(int currentDay) {
        if (_cachedDayData == null) {
            _cachedDayData = new Dictionary<int, DayData>();
        }

        if (!_cachedDayData.ContainsKey(currentDay)) {
            DayData dayData = dataHandler.Load();

            if (dayData != null)
            {
                _cachedDayData[currentDay] = dayData;
            }
            else
            {
                Debug.LogError($"Day data file not found or failed to load: {fileName}");
            }
        }
    }

    public void ClearCache() {
        _cachedDayData = null;
    }


    // public void SaveGame() {
    //     SaveData saveData = new SaveData {
    //         shelvingTaskCompleted = shelvingTaskCompleted,
    //         loaningTaskCompleted = loaningTaskCompleted
    //     };
    //     string json = JsonUtility.ToJson(saveData);
    //     System.IO.File.WriteAllText("savefile.json", json);
    // }

    // public void LoadGame() {
    //     if (System.IO.File.Exists("savefile.json")) {
    //         string json = System.IO.File.ReadAllText("savefile.json");
    //         SaveData saveData = JsonUtility.FromJson<SaveData>(json);
    //         // Load in data
    //         shelvingTaskCompleted = saveData.shelvingTaskCompleted;
    //         loaningTaskCompleted = saveData.loaningTaskCompleted;
    //         // Load the saved scene?
    //         // UnityEngine.SceneManagement.SceneManager.LoadScene(CurrentScene);
    //     }
    // }
}

[System.Serializable]
public class DayData
{
    public List<BookData> shelvingBooks;
    // public List<LoanRequest> loanRequests;

    // TODO: add people to talk to list/ load into explore state
}

public class BookData
{
    public string bookTitle; 
    public int reputationScore;
    public int bookID;
}

// public class LoanRequest
// {
//     public Book_ book;
//     public Patron_ patron;
// }