using UnityEngine;

public class PlayerNoteInteraction : MonoBehaviour
{
    public GameObject noteUIPanel; // Drag your UI panel into this field in the Inspector

    public GameObject currentNote; // Tracks the note the player is near

    void Update()
    {
        // Check for interaction input (e.g., mouse click)
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            if (currentNote != null) // If near a note
            {
                OpenNoteUI();
            }
        }

        // Close the UI when clicking anywhere else
        if (Input.GetMouseButtonDown(0) && noteUIPanel.activeSelf)
        {
            CloseNoteUI();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player is near a note
        if (other.CompareTag("Note"))
        {
            currentNote = other.gameObject; // Save the note reference
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Clear the note reference when the player leaves the trigger area
        if (other.CompareTag("Note"))
        {
            currentNote = null;
        }
    }

    void OpenNoteUI()
    {
        // Activate the UI panel
        noteUIPanel.SetActive(true);

        // Optionally, hide the note in the scene
        currentNote.SetActive(false);
    }

    void CloseNoteUI()
    {
        // Deactivate the UI panel
        noteUIPanel.SetActive(false);

        // Remove the note from the scene
        if (currentNote != null)
        {
            Destroy(currentNote);
            currentNote = null;
        }
    }
}