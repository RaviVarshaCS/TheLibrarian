using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Shelving : MonoBehaviour
{
    
    public Button returnToLibrary; 
    void Start()
    {
        // Set up button listeners
        if (returnToLibrary != null)
        {
            returnToLibrary.onClick.AddListener(OnReturnButtonClick);
        }
        
    }

    private void OnReturnButtonClick()
    {
        if(GameManager.Instance != null)
        {
            GameManager.Instance.UpdateGameState(GameState.MainLibrary);
        }
        Debug.Log("Start button clicked");
    }
}
