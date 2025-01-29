using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ShelvingDecision {
    Restricted,
    Public,
    Hide 
}

public class LibraryShelvingState : LibraryBaseState
{
    public List<BookData> shelvingBooks;
    private BookData currentBook;

    // reputation is updated after shelving task is completed
    // so that the player can't just quit the game and restart
    // we would write restriction as a bool in the Book_ class
    private int tempReputation;
    private bool readingView;


    // when book is clicked on, need to be in readingview
    // when game is paused, need to come back to readingview
    // or currBook

    public override void EnterState(LibraryDayManager library)
    {
        shelvingBooks = LibraryDayManager.Instance.dayData.shelvingBooks;

        // restore saved data if applicable???
    }
    public override void UpdateState(LibraryDayManager library)
    {

        // shelving logic ...
        // cycle through shelvingBooks
        // after decision is cemented for the day, update the GameInstance.reputation
        // shelvingTaskCompleted = true;

        // if (shelvingTaskCompleted) {
        //     library.SwitchState(library.ExplorationState);
        // }
    }

    // is it any worth to have a exit state method?
}

// public class ShelvingSaveData {
//     public List<BookData> shelvingBooks;
//     public List<BookDecision> shelvingDecisions;
//     public bool shelvingTaskCompleted;
//     private BookData currentBook;
//     private int tempReputation;
//     private bool readingView;
// }

public class BookDecision {
    public int bookID;
    public ShelvingDecision shelvingDecision;
}