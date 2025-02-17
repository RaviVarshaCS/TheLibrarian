using UnityEngine;

[CreateAssetMenu(fileName = "NewBook", menuName = "Book")]
public class Book_ : ScriptableObject
{
    public string bookTitle; 
    public Sprite[] pages;
    //public Sprite bookCover;
    public int reputationScore;
    public int bookID;
}