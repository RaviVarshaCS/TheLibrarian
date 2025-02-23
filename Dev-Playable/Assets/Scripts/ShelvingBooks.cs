using UnityEngine;

public class ShelvingBooks : MonoBehaviour
{
   [SerializeField] private BookDataSO bookDataSO;

   // cm 3:22 expose it?
    public void SetBookData(BookDataSO bookData)
    {
        bookDataSO = bookData;
    }
}
