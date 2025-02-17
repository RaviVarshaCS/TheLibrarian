// using UnityEngine;

// public class NoteManager : MonoBehaviour
// {
//     public GameObject noteUICanvas; // Assign the UI panel in the Inspector
//     private GameObject activeNote; // Tracks the currently found note

//     void Update()
//     {
//         // Detect player clicking
//         if (Input.GetMouseButtonDown(0)) // Left-click
//         {
//             if (noteUICanvas.activeSelf) // If the UI is already open, close it
//             {
//                 CloseNoteUI();
//             }
//             else // Otherwise, check for clicking on the note
//             {
//                 HandleNoteClick();
//             }
//         }
//     }

//     private void HandleNoteClick()
//     {
//         // Raycast from the mouse position
//         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//         if (Physics.Raycast(ray, out RaycastHit hit))
//         {
//             Debug.Log($"Raycast hit: {hit.collider.gameObject.name}");

//             if (hit.collider.CompareTag("SecretNote"))
//             {
//                 Debug.Log("Found Secret Note!");
//                 OpenNoteUI(hit.collider.gameObject);
//             }
//         }
//         else
//         {
//             Debug.Log("Raycast did not hit anything.");
//         }
//     }

//     void OpenNoteUI(GameObject note)
//     {
//         if (noteUICanvas != null)
//         {
//             // Activate the UI panel
//             noteUICanvas.SetActive(true);

//             // Store the note reference
//             activeNote = note;

//             // Optionally, disable the note object to prevent further interaction
//             note.SetActive(false);
//         }
//         else
//         {
//             Debug.LogWarning("Note UI Canvas is not assigned in the Inspector.");
//         }
//     }

//     public void CloseNoteUI()
//     {
//         if (noteUICanvas != null)
//         {
//             // Deactivate the UI panel
//             noteUICanvas.SetActive(false);

//             // Remove the note from the library scene
//             if (activeNote != null)
//             {
//                 Destroy(activeNote);
//                 activeNote = null;
//             }
//         }
//         else
//         {
//             Debug.LogWarning("Note UI Canvas is not assigned in the Inspector.");
//         }
//     }
// }