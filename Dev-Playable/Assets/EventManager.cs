using UnityEngine;
using UnityEngine.EventSystems;

public class EventManager : MonoBehaviour
{
    private static EventManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}