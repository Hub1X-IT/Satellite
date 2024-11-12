using System;
using UnityEngine;
using UnityEngine.UI;

public class EnterableUIObject : MonoBehaviour
{
    [SerializeField]
    private Button backButton;

    private Action closeAction;

    private void Awake()
    {
        backButton.onClick.AddListener(() =>
        {
            Disable();
            closeAction();
        });
    }

    public void Enable(Action onCloseAction)
    {
        closeAction = onCloseAction;

        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}