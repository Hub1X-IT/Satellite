using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseClick
{
    // Script from ChatGPT XD
    public static void SimulateClick(Vector2 screenPosition)
    {
        // Create a pointer event
        PointerEventData pointerData = new(EventSystem.current)
        {
            position = screenPosition
        };

        // Debug log !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        // Debug.Log(screenPosition);

        // Create a list to hold the results of the raycast
        List<RaycastResult> raycastResults = new();

        // Raycast using the EventSystem
        EventSystem.current.RaycastAll(pointerData, raycastResults);

        // Iterate through the results to interact with UI elements
        foreach (var result in raycastResults)
        {
            // Execute the pointer click event
            // Debug log !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            // Debug.Log(result);
            ExecuteEvents.Execute(result.gameObject, pointerData, ExecuteEvents.pointerClickHandler);
        }
    }
}
