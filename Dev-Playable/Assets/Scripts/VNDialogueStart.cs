using UnityEngine;
using TMPro;
using System.Collections;

public class VNDialogueStart : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // Reference to the TextMeshPro UI element
    public string[] dialogueLines;      // Array of dialogue lines
    private int currentLine = 0;        // Tracks the current dialogue line
    public float typingSpeed = 0.05f;   // Speed of the typing effect
    private bool isTyping = false;      // Checks if typing is in progress
    private bool isCanvasOpen = false;  // Tracks if the UI canvas is open

    public GameObject bookUIPanel;      // Reference to the UI panel for book decision
    public GameObject openUIPanelButton; // Button to open the UI panel

    void Start()
    {
        // Ensure the book decision UI is hidden initially
        bookUIPanel.SetActive(false);
        openUIPanelButton.SetActive(true);

        // Start the first line of dialogue
        ShowNextLine();
    }

    void Update()
    {
        // Prevent dialogue progression if the UI canvas is open
        if (isCanvasOpen)
            return;

        // Proceed to the next line when the screen is clicked
        if (Input.GetMouseButtonDown(0) && !isTyping)
        {
            ShowNextLine();
        }
        else if (Input.GetMouseButtonDown(0) && isTyping)
        {
            // Skip typing effect and display the full line
            StopAllCoroutines();
            if (currentLine < dialogueLines.Length) // Prevent index out of range
            {
                dialogueText.text = dialogueLines[currentLine];
            }
            isTyping = false;
        }
    }

    public void ShowNextLine()
    {
        if (currentLine < dialogueLines.Length)
        {
            // After the first line, show the button
            if (currentLine == 1)
            {
                openUIPanelButton.SetActive(true);
            }

            StartCoroutine(TypeSentence(dialogueLines[currentLine]));
            currentLine++;
        }
        else
        {
            // Clear text when finished
            dialogueText.text = "";
            Debug.Log("End of dialogue.");
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = ""; // Clear the text box before typing
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    public void OpenBookUIPanel()
    {
        // Show the book decision UI panel and hide the open button
        if (bookUIPanel != null && openUIPanelButton != null)
        {
            bookUIPanel.SetActive(true);  // Make the book decision UI visible
            openUIPanelButton.SetActive(false); // Hide the button that opened it
            isCanvasOpen = true; // Prevent further dialogue progression
        }
        else
        {
            Debug.LogWarning("BookUIPanel or OpenUIPanelButton is not assigned in the Inspector.");
        }
    }

    public void MakeChoice(string choice)
    {
        // Log the player's choice
        if (choice == "Give Book")
        {
            Debug.Log("Player chose to Give the book.");
            // currentLine++; // Skip to the dialogue after the next
            ShowNextLine();
             // Print the next line of dialogue
        }
        else if (choice == "Don't Give Book")
        {
            Debug.Log("Player chose Don't Give.");
            currentLine++; // Skip to the dialogue after the next
            // ShowNextLine(); // Print the skipped line
            ShowNextLine();
        }

        // Close the book decision UI
        if (bookUIPanel != null)
        {
            bookUIPanel.SetActive(false); // Deactivate the panel
            isCanvasOpen = false; // Allow dialogue progression again
        }
        else
        {
            Debug.LogWarning("bookUIPanel is not assigned in the Inspector.");
        }
    }
}