using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoGameManager : MonoBehaviour {
    public static DemoGameManager Instance; // Singleton instance

    // List of scenes for the demo
    private string[] demoScenes = {
        "Main Menu",
        "Introduction", 
        "Walking to Library",
        "Main Library",
        "Shelving Task",
        "Main Library",
        "Talking",
        "Main Library"
    };

    private int currentSceneIndex = 0; // Tracks the current scene in the sequence
    private PersistentUI ui; // Reference to the PersistentUI

    void Awake() {
        // Singleton pattern to ensure only one DemoGameManager exists
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    void Start() {
        // Load the first scene in the demo sequence
        LoadScene(demoScenes[currentSceneIndex]);
    }

    void Update() {
        // Navigate scenes using the A and D keys
        if (Input.GetKeyDown(KeyCode.D)) {
            GoToNextScene();
        } else if (Input.GetKeyDown(KeyCode.A)) {
            GoToPreviousScene();
        }

        // Quit the game with the Escape key
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }

    private void GoToNextScene() {
        if (currentSceneIndex < demoScenes.Length - 1) {
            currentSceneIndex++;
            LoadScene(demoScenes[currentSceneIndex]);
        }
    }

    private void GoToPreviousScene() {
        if (currentSceneIndex > 0) {
            currentSceneIndex--;
            LoadScene(demoScenes[currentSceneIndex]);
        }
    }

    private void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);

        // Initialize the Persistent UI when entering Main Library for the first time
        if (sceneName == "Main Library" && ui == null) {
            InitializePersistentUI();
        }
    }

    private void InitializePersistentUI() {
        GameObject uiPrefab = Resources.Load<GameObject>("Prefabs/PersistentUI");
        if (uiPrefab != null) {
            GameObject uiInstance = Instantiate(uiPrefab);
            DontDestroyOnLoad(uiInstance); // Ensure the UI persists across scenes
            ui = uiInstance.GetComponent<PersistentUI>();
        } else {
            Debug.LogError("PersistentUI prefab not found in Resources/Prefabs!");
        }
    }

    public void AdjustReputation(int amount) {
        // Update the reputation value through PersistentUI
        if (ui != null) {
            ui.UpdateUI(ui.Reputation + amount, ui.Relationship);
        }
    }

    public void AdjustRelationship(int amount) {
        // Update the relationship value through PersistentUI
        if (ui != null) {
            ui.UpdateUI(ui.Reputation, ui.Relationship + amount);
        }
    }
}