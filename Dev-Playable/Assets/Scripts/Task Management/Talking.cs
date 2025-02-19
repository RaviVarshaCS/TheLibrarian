using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Talking : MonoBehaviour
{
    public GameObject readableViewUI;
    public Button publicButton;
    public Button restrictedButton;

    public TextMeshProUGUI dialogueText;
    public GameObject dialoguePanel;

    private bool isAtCenter = false;
    private bool isDialogueActive = true;

    public List<BookData> currDayData;
    public int currentBook = 0;
    public string[] currDialogue;
    public bool finishedReading = false;
    private int dialogueIndex = 0;

    private void Start()
    {
        if (readableViewUI != null) readableViewUI.SetActive(false);
        if (dialoguePanel != null) dialoguePanel.SetActive(false);

        if (publicButton != null) publicButton.onClick.AddListener(OnPublicButtonClick);
        if (restrictedButton != null) restrictedButton.onClick.AddListener(OnRestrictedButtonClick);

        getAllDayContents();
    }

    public void getAllDayContents()
    {
        int runningDay = GameManager.Instance.currDay();
        currDayData = BookDatabase.Instance.GetTalkingBooksForDay(runningDay);
        Debug.Log("All Day contents received");

        if(TalkingManager.Instance.firstTalk == true) {
            currentBook = 0;
        } else {
            currentBook = 1;
        }

        currDialogue = currDayData[currentBook].dialogue;
        StartDialogue(currDialogue);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isDialogueActive)
        {
            AdvanceDialogue(currDialogue);
        }
    }

    public void StartDialogue(string[] dia)
    {
        dialogueIndex = 0;
        isDialogueActive = true;
        dialoguePanel.SetActive(true);
        dialogueText.text = dia[dialogueIndex];
    }

    public void AdvanceDialogue(string[] dia)
    {
        dialogueIndex++;
        if (dialogueIndex < dia.Length)
        {
            dialogueText.text = dia[dialogueIndex];
        }
        else
        {
            if(!finishedReading) {
                isDialogueActive = false;
                dialoguePanel.SetActive(false);
                OpenReadableView();
            } else {
                if(currentBook == 0) {
                    TalkingManager.Instance.finishedFirstTalk();
                } else {
                    GameManager.Instance.CompleteTask("Patron");
                    
                }
                
            }
        }
    }

    private void OpenReadableView()
    {
        if (readableViewUI != null)
        {
            readableViewUI.SetActive(true);
        }
        Debug.Log("Opened readable view.");
    }

    private void CloseReadableView()
    {
        if (readableViewUI != null)
        {
            readableViewUI.SetActive(false);
        }
        Debug.Log("Closed readable view.");
    }

    private void OnPublicButtonClick()
    {
        Debug.Log("Public Button Clicked!");
        GameManager.Instance.ModifyReputation(currDayData[currentBook].publicReputation);
        GameManager.Instance.ModifyRelationship(currDayData[currentBook].publicRelationship);
        RemoveBookFromDesk();
        CloseReadableView();
        finishedReading = true;

        currDialogue = currDayData[currentBook].giveBook;
        StartDialogue(currDialogue);
    }

    private void OnRestrictedButtonClick()
    {
        Debug.Log("Restricted Button Clicked!");
        GameManager.Instance.ModifyReputation(currDayData[currentBook].restrictedReputation);
        GameManager.Instance.ModifyRelationship(currDayData[currentBook].restrictedRelationship);
        RemoveBookFromDesk();
        CloseReadableView();
        finishedReading = true;

        currDialogue = currDayData[currentBook].dontGiveBook;
        StartDialogue(currDialogue);
    }

    private void RemoveBookFromDesk()
    {
        if (isAtCenter)
        {
            Debug.Log("Book removed from desk.");
            gameObject.SetActive(false);
            isAtCenter = false;
        }
    }
}
