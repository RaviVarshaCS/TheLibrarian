using UnityEngine;
using UnityEngine.UI;

public class PersistentUI : MonoBehaviour {
    public Slider reputationBar;
    public Slider relationshipBar;

    public int Reputation { get; private set; } = 50;
    public int Relationship { get; private set; } = 50;

    public void UpdateUI(int reputation, int relationship) {
        Reputation = Mathf.Clamp(reputation, 0, 100);
        Relationship = Mathf.Clamp(relationship, 0, 100);

        if (reputationBar != null) {
            reputationBar.value = Reputation;
        }

        if (relationshipBar != null) {
            relationshipBar.value = Relationship;
        }
    }
}