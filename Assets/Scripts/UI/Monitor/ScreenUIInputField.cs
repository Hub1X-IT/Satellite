using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScreenUIInputField : MonoBehaviour, IScreenUIInteractable
{
    private TMP_InputField inputField;

    private ScreenUICursorController cursorController;

    private List<GameObject> characterColliders;

    private Vector2 lastCharacterPosition;

    private float previousTextLength;

    public Transform SelfTransform { get; private set; }

    /*
    public event Action OnRightClick;

    public event Action OnLeftClick;
    */

    private void Awake()
    {
        inputField = GetComponent<TMP_InputField>();

        cursorController = GetComponentInParent<MonitorUI>().GetComponentInChildren<ScreenUICursorController>();

        characterColliders = new();
        lastCharacterPosition = Vector2.zero;

        SelfTransform = transform;

        inputField.onDeselect.AddListener((_) => Debug.Log(name + ": inputField.onDeselect"));
        
        /*
        inputField.onValueChanged.AddListener(InputFieldValueChanged);
        */
    }

    /*
    private void OnDestroy()
    {
        OnLeftClick = null;
        OnRightClick = null;
    }
    */

    public void SetHighlighted(bool highlighted) { }

    public void LeftClick()
    {
        inputField.ActivateInputField();
        /*
        OnLeftClick?.Invoke();
        */
    }

    public void RightClick()
    {
        Debug.Log($"{name}: right click");
        /*
        OnRightClick?.Invoke();
        */
    }

    /*
    private void InputFieldValueChanged(string text)
    {
        if (text.Length > previousTextLength)
        {

            inputField.fontAsset.sourceFontFile.GetCharacterInfo(text[text.Length - 1], out CharacterInfo lastCharacterInfo);
            float lastCharacterWidth = lastCharacterInfo.glyphWidth;
            GameObject lastCharacterCollider = Instantiate(new GameObject());
            lastCharacterCollider.transform.SetParent(inputField.textComponent.transform);
            RectTransform instantiatedObjectRectTransform = lastCharacterCollider.AddComponent<RectTransform>();

            if (lastCharacterPosition.x + lastCharacterWidth >
                inputField.textComponent.rectTransform.anchoredPosition.x + inputField.textComponent.rectTransform.sizeDelta.x)
            {
                // new line
                lastCharacterPosition.y += inputField.fontAsset.sourceFontFile.lineHeight;
            }

            instantiatedObjectRectTransform.anchoredPosition = lastCharacterPosition;
            lastCharacterPosition.x += lastCharacterWidth;

            characterColliders.Add(lastCharacterCollider);
        }
        previousTextLength = text.Length;
    }
    */
}
