using UnityEngine;
using TMPro;

public class PlayerReputationText : MonoBehaviour
{
    public TextMeshProUGUI reputationText;

    void Awake()
    {
        SetReputation(50);
    }

    public void SetReputation(int reputation)
    {
        reputationText.text = "" + reputation;
    }

    public void UpdateReputation(Component sender, object data)
    {
        if (data is int){
            int amount = (int)data;
            SetReputation(amount);
        }
        
    }
}
