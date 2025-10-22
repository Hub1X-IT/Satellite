using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CopyPasteMenuUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public enum MenuFunction
    {
        CopyMenu,
        PasteMenu,
    }

    public event Action<string> CopiedText;
    public event Action<string> PastedText;

    private MenuFunction menuFunction;

    private TMP_InputField contentField;

    private RectTransform rectTransform;

    [SerializeField]
    private Button copyPasteButton;

    private bool canDisableCopyMenu;

    public void InitializeCopyPasteMenuUI(MenuFunction function, TMP_InputField contentInputField)
    {
        menuFunction = function;

        contentField = contentInputField;
        rectTransform = GetComponent<RectTransform>();


        contentField.onDeselect.AddListener((_) =>
        {
            if (canDisableCopyMenu)
            {
                SetCopyPasteMenuEnabled(false);
            }
        });
        copyPasteButton.onClick.AddListener(() =>
        {
            if (menuFunction == MenuFunction.CopyMenu)
            {
                CopyText();
            }
            else if (menuFunction == MenuFunction.PasteMenu)
            {
                PasteText();
            }
            SetCopyPasteMenuEnabled(false);
        });

        canDisableCopyMenu = true;
    }

    void OnDestroy()
    {
        CopiedText = null;
        PastedText = null;
    }

    // May be temporary
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Debug.Log("enter" + eventData.position);
        canDisableCopyMenu = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Debug.Log("exit" + eventData.position);
        canDisableCopyMenu = true;
    }

    public void SetCopyPasteMenuEnabled(bool enabled)
    {
        gameObject.SetActive(enabled);
        canDisableCopyMenu = true;
    }

    private void MoveCopyPasteMenu(Vector2 newPosition)
    {
        rectTransform.anchoredPosition = newPosition;
    }

    private void CopyText()
    {
        string textToCopy = contentField.text;
        VirtualClipboard.SetClipboardText(textToCopy);
        CopiedText?.Invoke(textToCopy);
    }

    private void PasteText()
    {
        string textToPaste = VirtualClipboard.GetClipboardText();
        contentField.text = textToPaste;
        PastedText?.Invoke(textToPaste);
    }

}