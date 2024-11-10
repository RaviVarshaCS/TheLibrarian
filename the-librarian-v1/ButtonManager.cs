using UnityEngine;
using UnityEngine.UI;


public class ButtonManager : MonoBehaviour
{
    // Reference to the buttons
    public Button hideBookButton;
    public Button shelveBookButton;   
    public Button reportBookButton;


    // Start is called before the first frame update
    void Start()
    {
        // Add listeners for button clicks
        hideBookButton.onClick.AddListener(OnHideBookClicked);
        shelveBookButton.onClick.AddListener(OnShelveBookClicked);
        reportBookButton.onClick.AddListener(OnReportBookClicked);
    }


    // Method called when the Hide Book button is clicked
    private void OnHideBookClicked()
    {
        HideButton(hideBookButton);
    }


    // Method called when the Shelve Book button is clicked
    private void OnShelveBookClicked()
    {
        HideButton(shelveBookButton);
    }


    // Method called when the Report Book button is clicked
    private void OnReportBookClicked()
    {
        HideButton(reportBookButton);
    }


    // Method to hide a button
    private void HideButton(Button button)
    {
        button.gameObject.SetActive(false);
    }
}
