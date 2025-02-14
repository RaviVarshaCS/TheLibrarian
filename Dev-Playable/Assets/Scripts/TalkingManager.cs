using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

/*
To-do: Connect to talking button, and set-up possible time-based notifications
*/

public class TalkingManager : MonoBehaviour

{
    public static TalkingManager Instance { get; set; }

    public bool firstTalk;



    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }

        firstTalk = true;
    }

    public void finishedFirstTalk() {
        firstTalk = false;
    }

    public bool isFirstTalk() {
        return firstTalk;
    }

}