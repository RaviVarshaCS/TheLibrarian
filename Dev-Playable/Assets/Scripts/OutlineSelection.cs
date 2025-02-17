using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OutlineSelection : MonoBehaviour
{
    private Transform highlight;
    private Transform selection;

    void Update()
    {
        // Check if the mouse is over a UI element
        if (EventSystem.current.IsPointerOverGameObject())
        {
            ResetHighlight();
            return;
        }

        // Detect GameObject under mouse cursor
        if (Input.GetMouseButtonDown(0)) // Left-click for selection
        {
            HandleSelection();
        }
        else
        {
            HandleHighlight();
        }
    }

    private void HandleHighlight()
    {
        // Get the object under the mouse
        GameObject hoveredObject = GetObjectUnderMouse();

        // Reset previous highlight if needed
        if (highlight != null && highlight != hoveredObject?.transform)
        {
            ResetHighlight();
        }

        if (hoveredObject != null && hoveredObject.CompareTag("Selectable"))
        {
            highlight = hoveredObject.transform;

            // Enable or add the outline
            Outline outline = highlight.GetComponent<Outline>() ?? highlight.gameObject.AddComponent<Outline>();
            outline.OutlineColor = Color.magenta;
            outline.OutlineWidth = 7.0f;
            outline.enabled = true;
        }
    }

    private void HandleSelection()
    {
        GameObject clickedObject = GetObjectUnderMouse();

        // Clear previous selection
        if (selection != null && selection != clickedObject?.transform)
        {
            ResetSelection();
        }

        if (clickedObject != null && clickedObject.CompareTag("Selectable"))
        {
            selection = clickedObject.transform;

            // Enable or add outline for selection
            Outline outline = selection.GetComponent<Outline>() ?? selection.gameObject.AddComponent<Outline>();
            outline.OutlineColor = Color.green; // Selection color
            outline.OutlineWidth = 7.0f;
            outline.enabled = true;
        }
        else
        {
            ResetSelection();
        }
    }

    private GameObject GetObjectUnderMouse()
    {
        // Cast a ray from the mouse position into the scene
        Vector2 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.transform.gameObject;
        }

        return null;
    }

    private void ResetHighlight()
    {
        if (highlight != null)
        {
            Outline outline = highlight.GetComponent<Outline>();
            if (outline != null)
            {
                outline.enabled = false;
            }
            highlight = null;
        }
    }

    private void ResetSelection()
    {
        if (selection != null)
        {
            Outline outline = selection.GetComponent<Outline>();
            if (outline != null)
            {
                outline.enabled = false;
            }
            selection = null;
        }
    }
}
