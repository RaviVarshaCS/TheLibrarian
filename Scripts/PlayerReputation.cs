using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerReputation : MonoBehaviour, IDataPersistance
{
    public static PlayerReputation Instance  { get; private set; }
    private int startingReputation = 50;
    private int minReputation = 0;
    private int maxReputation = 100;

    [Header("Events")]
    public GameEvent onReputationUpdated;
    private int Reputation;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Ensure it persists across scenes
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
        Reputation = startingReputation;
    }

    public void UpdateReputation(int amount)
    {
        Reputation += amount;
        Reputation = Mathf.Clamp(Reputation, minReputation, maxReputation);
        onReputationUpdated.Raise(this, Reputation);
    }

    public void LoadData(GameData data)
    {
        this.Reputation = data.reputation;
    }

    public void SaveData(ref GameData data)
    {
        data.reputation = this.Reputation;
    }
}
