using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Patron_", menuName = "Scriptable Objects/Patron_")]
public class Patron_ : ScriptableObject
{
    public string patronName;
    public bool hasTalkedBefore;
    public List<string> pastInteractions;  // List to store event IDs or descriptions of past interactions.
    public Mesh patronMesh;
}
