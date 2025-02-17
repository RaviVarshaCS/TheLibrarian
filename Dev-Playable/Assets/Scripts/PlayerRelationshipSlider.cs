using UnityEngine;
using UnityEngine.UI;

public class PlayerRelationshipSlider : MonoBehaviour
{
    [SerializeField] private Slider relSlider;

    void Awake()
    {
        SetRelationship(50);
    }

    public void SetRelationship(int relationship)
    {
        relSlider.value = relationship / 100f;
    }

    public void UpdateRelationship(Component sender, object data)
    {
        if (data is int){
            int amount = (int)data;
            SetRelationship(amount);
        }
        
    }
}
