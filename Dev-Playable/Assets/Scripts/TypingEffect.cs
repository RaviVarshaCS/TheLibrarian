using UnityEngine;
using TMPro;
using System.Collections;
// does this work?
public class TypingEffect : MonoBehaviour 
{
    public TextMeshProUGUI textDisplay; // Drag your TextMeshPro object here in the Inspector
    [SerializeField] 
    private string[] sentences = {
        "The year is 1936.",
        "Militarism is on the rise as the government prepares for further expansion into China.",
        "Crackdown on political dissent following a Coup Attempt in February compromises citizens’ individual liberties.",
        "Word is in the air of a Pact in the works with Nazi Germany.",
        "You are the sole librarian in a small rural village - it is your duty to uphold censorship law.",
        "Do not disappoint your fellow countrymen."
    };
    public float typingSpeed = 0.05f; // Speed of typing

    private int index = 0;

    void Start()
    {
        StartCoroutine(TypeSentence());
    }

    IEnumerator TypeSentence()
    {
        if (index >= sentences.Length) // Prevent out-of-bounds access
        {
            EndIntro();
            yield break; // Exit the coroutine
        }

        textDisplay.text = ""; // Clear text
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter; // Add one letter at a time
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(5f); // Wait 5 seconds after the sentence finishes

        index++; // Increment index
        StartCoroutine(TypeSentence()); // Move to the next sentence
    }

    void EndIntro()
    {
        Debug.Log("Intro Finished"); // Replace this with your next scene transition
    }
}
