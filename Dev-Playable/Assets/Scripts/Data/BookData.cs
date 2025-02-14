using UnityEngine;

[System.Serializable]
public class BookData
{
    public string bookName;
    public int publicReputation;
    public int restrictedReputation;
    public int publicRelationship;
    public int restrictedRelationship;
    public Sprite[] sprites;  // Book page sprites
    public string dialogue;   // Dialogue (empty for shelving tasks)
}
