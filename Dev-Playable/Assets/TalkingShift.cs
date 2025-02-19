using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class TalkingShift : MonoBehaviour
{
    public GameObject bookSlipEmpty;
    public GameObject bookSlipFull;
    

    void Start()
    {
        bookSlipEmpty.SetActive(true);
        bookSlipFull.SetActive(false);
        // // Set up button listeners
        // if (returnToLibrary != null)
        // {
        //     returnToLibrary.onClick.AddListener(OnReturnButtonClick);
        // }
    }

    void Update() {
        string currentScene = SceneManager.GetActiveScene().name;
        
        if(currentScene != "Shelving Task") {
            Debug.Log("Current Scene is " + currentScene);
            // bookSlipEmpty.SetActive(true);
            bookSlipFull.SetActive(true);
            bookSlipEmpty.SetActive(false);

        } else {
            bookSlipFull.SetActive(false);
            bookSlipEmpty.SetActive(true);
        }
        
    }

    // private void OnReturnButtonClick()
    // {
    //     if(GameManager.Instance != null)
    //     {
    //         GameManager.Instance.UpdateGameState(GameState.MainLibrary);
    //     }
    //     Debug.Log("Start button clicked");
    // }
}

