using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AppTitleBarUI : MonoBehaviour
{
    public event Action AppCloseTriggered;

    [SerializeField]
    private TMP_Text appNameTextField;

    [SerializeField]
    private Button appCloseButton;

    private void Awake()
    {
        appCloseButton.onClick.AddListener(() => AppCloseTriggered?.Invoke());
    }

    private void OnDestroy()
    {
        AppCloseTriggered = null;
    }

    public void SetAppName(string appName)
    {
        appNameTextField.text = appName;
    }
}