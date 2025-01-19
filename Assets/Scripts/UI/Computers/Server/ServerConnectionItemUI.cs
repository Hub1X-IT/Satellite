using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ServerConnectionItemUI : MonoBehaviour
{
    public event Action<ServerConnectionItemUI> ConnectionEnabled;
    public event Action ConnectionDisabled;

    [SerializeField]
    private Button toggleConnectionButton;

    [SerializeField]
    private Image serverIcon;

    [SerializeField]
    private TMP_Text connectionTextField;

    private bool isConnected;

    private const string DisconnectText = "Disconnect";
    private const string ConnectText = "Connect";

    private void Awake()
    {
        isConnected = false;
        toggleConnectionButton.onClick.AddListener(TryToggleConnection);
    }

    private void TryToggleConnection()
    {
        if (isConnected || !ServerConnectionManager.IsConnectionActive)
        {
            SetConnectionEnabled(!isConnected);
        }
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

    public void SetColor(Color color)
    {
        serverIcon.color = color;
        toggleConnectionButton.image.color = color;
    }

    public void SetInteractionEnabled(bool enabled)
    {
        toggleConnectionButton.interactable = enabled;
    }
}
