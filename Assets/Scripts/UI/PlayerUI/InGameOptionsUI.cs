using System;
using UnityEngine;
using UnityEngine.UI;

public class InGameOptionsUI : MonoBehaviour
{
    [SerializeField]
    private Button backButton;

    private Action inGameOptionsClosed;

    private void Awake()
    {
        backButton.onClick.AddListener(() =>
        {
            Disable();
            inGameOptionsClosed();
        });
    }

    public void Enable(Action onCloseAction)
    {
        inGameOptionsClosed = onCloseAction;

        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}