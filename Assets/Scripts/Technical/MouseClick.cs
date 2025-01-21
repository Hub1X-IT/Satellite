using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseClick
{
    private static readonly Type[] interactableTypes = {
        typeof(Button),
        typeof(TMP_InputField),
    };
    private static readonly Type[] interactionBlockingTypes = {
        typeof(Image),
    };

    public static void SimulateClick(Vector2 screenPosition)
    {
        PointerEventData pointerData = new(EventSystem.current)
        {
            position = screenPosition
        };

        List<RaycastResult> raycastResults = new();
        EventSystem.current.RaycastAll(pointerData, raycastResults);

        foreach (var result in raycastResults)
        {
            foreach (var type in interactableTypes)
            {
                if (result.gameObject.GetComponent(type) != null)
                {
                    Debug.Log($"Execute click on object: {result.gameObject.name} of type: {type.Name}");
                    ExecuteEvents.Execute(result.gameObject, pointerData, ExecuteEvents.pointerClickHandler);
                    return;
                }
            }
            foreach (var type in interactionBlockingTypes)
            {
                if (result.gameObject.GetComponent(type) != null)
                {
                    Debug.Log($"Interaction blocked by object: {result.gameObject.name} of type: {type.Name}");
                    return;
                }
            }
        }
    }
}
