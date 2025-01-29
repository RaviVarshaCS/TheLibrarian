using UnityEngine;
using TMPro;
using System.Collections;

public class VNDiag : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // Reference to the TextMeshPro UI element
    public string[] dialogueLines;      // Array of dialogue lines
    private int currentLine = 0;        // Tracks the current dialogue line
    public float typingSpeed = 0.05f;   // Speed of the typing effect
    private bool isTyping = false;      // Checks if typing is in progress

    void Start()
    {
        // Start the first line of dialogue
        ShowNextLine();
    }

    void Update()
    {
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
}