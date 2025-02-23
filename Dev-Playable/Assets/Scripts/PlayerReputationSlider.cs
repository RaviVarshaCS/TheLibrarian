using UnityEngine;
using UnityEngine.UI;

public class PlayerReputationSlider : MonoBehaviour
{
    [SerializeField] private Slider repSlider;

    void Awake()
    {
        SetReputation(50);
    }

    public void SetReputation(int reputation)
    {
        repSlider.value = reputation / 100f;
    }

    public void UpdateReputation(Component sender, object data)
    {
        if (data is int){
            int amount = (int)data;
            SetReputation(amount);
        }
        
    }
}
