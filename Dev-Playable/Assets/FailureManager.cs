using UnityEngine;

public class FailureManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    // Update is called once per frame
    public void onClickTry()
    {
        GameManager.Instance.resetGame();
    }
}
