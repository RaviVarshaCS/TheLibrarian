using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public GameObject noteUICanvas; // Assign in the Inspector
    private GameObject activeNote; // Tracks the currently found note

    void Update()
    {
        // Detect player clicking
        if (Input.GetMouseButtonDown(0)) // Left-click
        {
            if (noteUICanvas.activeSelf) // If the UI is already open, close it
            {
                CloseNoteUI();
            }
            else // Otherwise, check for clicking on the note
            {
                // Raycast from the mouse position
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.CompareTag("SecretNote"))
                    {
                        Debug.Log("Found Secret Note!");
                        OpenNoteUI(hit.collider.gameObject);
                    }
                }
            }
        }
    }

    void OpenNoteUI(GameObject note)
    {
        // Activate the UI panel
        noteUICanvas.SetActive(true);

        // Store the note reference
        activeNote = note;

        // Optionally, disable the note object to prevent further interaction
        note.SetActive(false);
    }

    public void CloseNoteUI()
    {
        // Deactivate the UI panel
        noteUICanvas.SetActive(false);

        // Remove the note from the library scene
        if (activeNote != null)
        {
            Destroy(activeNote);
            activeNote = null;
        }
    }
}