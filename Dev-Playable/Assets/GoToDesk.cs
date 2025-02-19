// using UnityEngine;
// using UnityEngine.UI;
// using System.Collections;

// public class GoToDesk : MonoBehaviour
// {
    
//     public Button goToDesk; 
//     void Start()
//     {
//         // Set up button listeners
//         if (goToDesk != null)
//         {
//             goToDesk.onClick.AddListener(goToDeskLibrary);
//         }
        
//     }

//     private void goToDeskLibrary()
//     {
//         if(GameManager.Instance != null)
//         {
//             GameManager.Instance.UpdateGameState(GameState.InitialTalk);
//         }
//         Debug.Log("Start button clicked");
//     }
// }
