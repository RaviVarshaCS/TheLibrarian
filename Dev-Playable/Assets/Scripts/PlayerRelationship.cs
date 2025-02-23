// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Events;

// public class PlayerRelationship : MonoBehaviour, IDataPersistance
// {
//     public static PlayerRelationship Instance  { get; private set; }
//     private int startingRelationship = 50;
//     private int minRelationship = 0;
//     private int maxRelationship = 100;

//     [Header("Events")]
//     public GameEvent onRelationshipUpdated;
//     private int Relationship;
    
//     void Awake()
//     {
//         if (Instance == null)
//         {
//             Instance = this;
//             DontDestroyOnLoad(gameObject); // Ensure it persists across scenes
//         }
//         else
//         {
//             Destroy(gameObject); // Ensure only one instance exists
//         }
//         Relationship = startingRelationship;
//     }

//     public void UpdateRelationship(int amount)
//     {
//         Relationship += amount;
//         Relationship = Mathf.Clamp(Relationship, minRelationship, maxRelationship);
//         onRelationshipUpdated.Raise(this, Relationship);
//     }

//     public void LoadData(GameData data)
//     {
//         this.Relationship = data.relationship;
//     }

//     public void SaveData(ref GameData data)
//     {
//         data.relationship = this.Relationship;
//     }
// }
