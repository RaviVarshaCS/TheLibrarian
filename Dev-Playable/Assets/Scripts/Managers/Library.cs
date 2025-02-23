using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.EventSystems;
/*
Attach this script to an empty in scene

Checks current day completion on start -> performs completion coroutine if day is complete after note
add note open/close functionality here
figure out timed notifications for talking to patrons (only after shelving scene is done)

check current day completion
shelving complete, don't allow move to shelving scene if 
add note data pull in later

FIX DESK CLICKING

Hover feature works now
*/

public class Library : MonoBehaviour
{
    public GameObject targetObject; // Assign object in Inspector
    private Renderer targetRenderer;
    public Color highlightColor = Color.yellow;
    private Color originalColor;
    public bool hoverEnabled = true; // Toggle effect

    void Start() {
        // To-do: Uncomment this when GameManager script is up and running
        GameManager.Instance.CheckDayCompletion();
        
        targetRenderer = targetObject.GetComponent<Renderer>();

        if (targetRenderer != null)
        {
            originalColor = targetRenderer.material.color;
        }


    }

    void Update()
    {
        if (!hoverEnabled || targetRenderer == null) return;

        // Prevent interaction if the mouse is over UI
        if (EventSystem.current.IsPointerOverGameObject()) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == targetObject)
            {
                targetRenderer.material.color = highlightColor;

                if (Input.GetMouseButtonDown(0)) // 0 = Left Click
                {
                    Debug.Log("Desk Clicked!");
                    if (!GameManager.Instance.shelvingCompleted) {
                        GameManager.Instance.LoadScene("Shelving Task");
                    }
                }
                return;
            }
        }

        targetRenderer.material.color = originalColor;
    }

}