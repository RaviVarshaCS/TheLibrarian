using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public Button talk;
    public Button returnToLibrary;
    public Button finishDay;
    public Button inventory;
    public Slider reputationSlider;
    public Slider relationshipSlider;
    public Button toTalking;

    public TextMeshProUGUI rep;
    public TextMeshProUGUI rel;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // Move here
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        if (returnToLibrary != null)
        {
            returnToLibrary.onClick.AddListener(returnButton);
        }

        if (toTalking != null) {
            toTalking.onClick.AddListener(onToTalking);
        }
        UpdateUI(); // Initialize UI values
    }

    public void onToTalking() {
        if(GameManager.Instance.patronInteractionCompleted == false) {
            GameManager.Instance.LoadScene("Initial Talk");
        }
    }

    void Update()
    {
        UpdateUI(); // Continuously update UI text
    }

    void UpdateUI()
    {
        if (rep != null && rel != null)
        {
            rep.text = $"{GameManager.Instance.reputation}";
            rel.text = $"{GameManager.Instance.relationship}";
        }

        if (reputationSlider != null)
        {
            reputationSlider.value = GameManager.Instance.reputation;
        }

        if (relationshipSlider != null)
        {
            relationshipSlider.value = GameManager.Instance.relationship;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main Menu" || scene.name == "FailureScene") // Adjust for failure scenes
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    public void returnButton()
    {
        GameManager.Instance.LoadScene("Library");
    }
}
