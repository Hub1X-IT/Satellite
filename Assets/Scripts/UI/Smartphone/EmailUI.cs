using System;
using UnityEngine;
using UnityEngine.UI;

public class EmailUI : MonoBehaviour
{
    [SerializeField] private Button emailExitButton;

    private Action emailOverviewClosed;


    private void Awake()
    {
        emailExitButton.onClick.AddListener(() =>
        {
            emailOverviewClosed();
            Disable();
        });
    }

    public void Enable(Action onCloseAction)
    {
        emailOverviewClosed = onCloseAction;

        gameObject.SetActive(true);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}