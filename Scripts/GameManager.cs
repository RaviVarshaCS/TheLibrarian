using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.Events;

// TODO
// - || ADD EOD STATS ||
// - Maybe add intro sequence as its own state?
// - add credits state
public enum GameState {
    MainMenu,
    Tutorial,
    LibraryWorkday,
    MainLibrary,
    Ending,
    Pause,
    Credits
}

public class GameManager : MonoBehaviour, IDataPersistance
{
    public static GameManager Instance { get; private set; }
    public GameState CurrentState;

    [SerializeField] private SceneLoader sceneLoader;

    [Header("Events")]
    public GameEvent onGameStateChange;

    [Header("Game Data")]
    [SerializeField] public int CurrentDay;

    // TODO:
    // - Add note functionality
    //public HashSet<string> collectedNotes = new HashSet<string>();

    [Header("SceneGroup-to-State Mapping")]
    [SerializeField] private List<SceneGroupStateMapping> sceneGroupStateMappings;


    public GameState GetGameStateForSceneType(string sceneGroupName) {
        var mapping = sceneGroupStateMappings.FirstOrDefault(m => m.sceneGroupName == sceneGroupName);
        return mapping?.GameState ?? throw new Exception($"No GameState mapped for {sceneGroupName}");
    }

    public int GetSceneGroupIndex(GameState gameState) {
        var mapping = sceneGroupStateMappings.FirstOrDefault(m => m.GameState == gameState);
        return mapping == null ? -1 : sceneGroupStateMappings.IndexOf(mapping);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            // DataPersistanceManager.instance.LoadGame();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateGameState(GameState.MainMenu);
    }

    public async void UpdateGameState(GameState newState)
    {
        if (newState == CurrentState) return;

        Debug.Log($"Changing GameState from {CurrentState} to {newState}.");

        switch (newState)
        {
            case GameState.MainMenu:
                //
                break;
            case GameState.Tutorial:
                //
                break;
            case GameState.LibraryWorkday:
                //
                break;
            case GameState.Ending:
                //
                break;
            case GameState.Pause:
                //
                break;
            case GameState.Credits:
                //
                break;
            case GameState.MainLibrary:
                //
                break;
            default:
                throw new System.Exception("Invalid game state");
        }

        await sceneLoader.LoadSceneGroup(GetSceneGroupIndex(newState));

        // WOULD ONLY CHANGE STATE AT THE END
        // so that we reflect proper state change
        CurrentState = newState;
        Debug.Log($"Game State: {CurrentState}");
        onGameStateChange.Raise(this, CurrentState);
    }

    // public void OnActiveSceneChanged(string sceneGroupName) {
    //     GameState newState = GetGameStateForSceneType(sceneGroupName);
    //     CurrentState = newState;
    //     Debug.Log($"ACTIVE SCENE CHANGE:::: {CurrentState}");
    //     onGameStateChange.Raise(this, CurrentState);
    // }

    public void LoadData(GameData data)
    {
        this.CurrentDay = data.currentDay;
    }

    public void SaveData(ref GameData data)
    {
        data.currentDay = this.CurrentDay;
    }
}

[System.Serializable]
public class SceneGroupStateMapping {
    public string sceneGroupName;
    public GameState GameState;
}