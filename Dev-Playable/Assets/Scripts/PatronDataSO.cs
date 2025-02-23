using UnityEngine;

[CreateAssetMenu()]
public class PatronDataSO : ScriptableObject
{
    public string patronName;
    public bool hasTalkedBefore;
    // public List<string> pastInteractions;  // List to store event IDs or descriptions of past interactions.
    // public Mesh patronMesh;
}
