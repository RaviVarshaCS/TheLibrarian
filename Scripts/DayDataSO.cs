using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class DayDataSO : ScriptableObject
{
    public List<BookDataSO> shelvingBooks;
    public List<LoanRequestSO> loanRequests;
}
