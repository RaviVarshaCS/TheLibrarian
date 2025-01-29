using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    
    public Button startGame; 
    public Button quitGame; 

    void Start()
    {
        // Set up button listeners
        if (startGame != null)
        {
            startGame.onClick.AddListener(OnStartButtonClick);
        }

        if (quitGame != null)
        {
            quitGame.onClick.AddListener(OnQuitButtonClick);
        }
        
    }

    private void OnStartButtonClick()
    {
        // for rn, lets go straight to shelving
        if(GameManager.Instance != null)
        {
            GameManager.Instance.UpdateGameState(GameState.LibraryWorkday);
        }
        Debug.Log("Start button clicked");
    }

    private void OnQuitButtonClick()
    {
        // // Quit the game
        // Application.Quit();
        Debug.Log("Quit button clicked");
    }
}
