using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ServerConnectionItem : MonoBehaviour
{
    public event Action<ServerConnectionItem> ConnectionEnabled;
    public event Action ConnectionDisabled;

    [SerializeField]
    private Button toggleConnectionButton;

    [SerializeField]
    private TMP_Text connectionTextField;

    private bool isConnected;

    private const string DisconnectText = "Disconnect";
    private const string ConnectText = "Connect";

    private void Awake()
    {
        isConnected = false;
        toggleConnectionButton.onClick.AddListener(() => SetConnectionEnabled(!isConnected));
    }

    private void SetConnectionEnabled(bool enabled)
    {
        isConnected = enabled;
        connectionTextField.text = enabled ? DisconnectText : ConnectText;

        if (enabled)
        {
            ConnectionEnabled?.Invoke(this);
        }
        else
        {
            ConnectionDisabled?.Invoke();
        }
    }
}
