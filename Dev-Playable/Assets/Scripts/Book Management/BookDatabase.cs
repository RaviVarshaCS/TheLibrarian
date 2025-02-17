
using System.Collections.Generic;
using UnityEngine;

public class BookDatabase : MonoBehaviour
{
    public static BookDatabase Instance { get; set; }

    // List of all books across all days
    public List<BookData> allBooks = new List<BookData>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist through scene changes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }

    // Get books for the current day
    public List<BookData> GetBooksForDay(int day) {
        List<BookData> dayBooks = new List<BookData>();

        foreach (var book in allBooks)
        {
            if (book.bookName.Contains("Day " + day)) // Check if the book corresponds to the day
            {
                dayBooks.Add(book);
            }
        }

        return dayBooks;
    }

    // Gets books for shelving
    public List<BookData> GetShelvingBooksForDay(int day)
    {
        List<BookData> forShelving = new List<BookData>();

        foreach (var book in allBooks)
        {
            if (book.bookName.Contains("Day " + day) && book.bookName.Contains("Shelving")) // Check if the book corresponds to the day
            {
                forShelving.Add(book);
            }
        }

        return forShelving;
    }

    // Gets books for talking 
    public List<BookData> GetTalkingBooksForDay(int day)
    {
        List<BookData> forTalking = new List<BookData>();

        foreach (var book in allBooks)
        {
            if (book.bookName.Contains("Day " + day) && book.bookName.Contains("Talking")) // Check if the book corresponds to the day
            {
                forTalking.Add(book);
            }
        }

        return forTalking;
    }
}
