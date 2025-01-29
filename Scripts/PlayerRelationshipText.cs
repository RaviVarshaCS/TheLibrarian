using UnityEngine;
using TMPro;

public class PlayerRelationshipText : MonoBehaviour
{
    public TextMeshProUGUI relationshipText;

    void Awake()
    {
        SetRelationship(50);
    }

    public void SetRelationship(int relationship)
    {
        relationshipText.text = "" + relationship;
    }

    public void UpdateRelationship(Component sender, object data)
    {
        if (data is int){
            int amount = (int)data;
            SetRelationship(amount);
        }
        
    }
}
