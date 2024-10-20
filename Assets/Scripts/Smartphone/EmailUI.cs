using System;
using UnityEngine;
using UnityEngine.UI;

public class EmailUI : MonoBehaviour {


    [SerializeField] private Button emailExitButton;


    private Action onCloseEmailOverviewAction;


    private void Awake() {
        emailExitButton.onClick.AddListener(() => {
            Hide();
            onCloseEmailOverviewAction();
        });
    }


    public void Show(Action onCloseEmailOverviewAction) {
        this.onCloseEmailOverviewAction = onCloseEmailOverviewAction;

        gameObject.SetActive(true);
    }


    public void Hide() {
        gameObject.SetActive(false);
    }
}