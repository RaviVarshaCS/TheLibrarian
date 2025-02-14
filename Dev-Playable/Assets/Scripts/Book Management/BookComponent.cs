using UnityEngine;

/*
    Attach this book component to each book in the game. 
    Sets the book data when called by task manager. 
*/

public class BookComponent : MonoBehaviour
{
    public BookData bookData; 

    public void SetBookData(BookData data)
    {
        bookData = data;
    }
}
