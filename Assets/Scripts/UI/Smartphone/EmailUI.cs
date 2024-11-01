using System;
using UnityEngine;
using UnityEngine.UI;

public class EmailUI : MonoBehaviour
{
    [SerializeField] private Button emailExitButton;


    private Action closingEmailOverview;


    private void Awake()
    {
        emailExitButton.onClick.AddListener(() =>
        {
            Hide();
            closingEmailOverview();
        });
    }

    private void OnDestroy()
    {
        closingEmailOverview = null;
    }

    public void Show(Action closingEmailOverview)
    {
        this.closingEmailOverview = closingEmailOverview;

        gameObject.SetActive(true);
    }


    private void Hide()
    {
        gameObject.SetActive(false);
    }
}