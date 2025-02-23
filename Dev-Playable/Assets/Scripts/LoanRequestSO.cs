using UnityEngine;

[CreateAssetMenu()]
public class LoanRequestSO : ScriptableObject
{
    public BookDataSO loanedBook;
    public PatronDataSO[] loanedToPatrons;
}
