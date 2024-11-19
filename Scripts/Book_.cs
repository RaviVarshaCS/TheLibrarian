using UnityEngine;

[CreateAssetMenu(fileName = "NewBook", menuName = "Book")]
public class Book_ : ScriptableObject
{
    public string bookTitle; 
    public Sprite[] pages;
}