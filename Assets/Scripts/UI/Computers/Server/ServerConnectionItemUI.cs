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
    [SerializeField]
    private TMP_Text connectionStatus;
    [SerializeField]
    private TMP_Text availableServers;
    [SerializeField]
    private TMP_Text serverID;

    public int availableServersNum;

    private const string disconnectedStatus = "<color=\"red\">Disconnected</color>";
    private const string connectedStatus = "<color=\"green\">Connected</color>";
    private const string nullServerID = "NULL";

    [SerializeField]
    private string[] serverIDList;

    private bool isConnected;

    private const string DisconnectText = "Disconnect";
    private const string ConnectText = "Connect";

    private void Awake()
    {
        isConnected = false;
        toggleConnectionButton.onClick.AddListener(TryToggleConnection);
        connectionStatus.text = disconnectedStatus;
        availableServers.text = availableServersNum.ToString();
        serverID.text = nullServerID;

        SetServerIDs(availableServersNum, serverIDList);
    }

    private void TryToggleConnection()
    {
        if (isConnected || !ServerConnectionManager.Instance.IsConnectionActive)
        {
            SetConnectionEnabled(!isConnected);
        }
    }

    private void SetConnectionEnabled(bool enabled)
    {
        isConnected = enabled;
        connectionTextField.text = enabled ? DisconnectText : ConnectText;
        availableServers.text = enabled ? availableServersNum.ToString() : (availableServersNum - 1).ToString();
        SetServerIDs(availableServersNum, serverIDList);
        serverID.text = enabled ? nullServerID : serverIDList[availableServersNum];

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

    private void SetServerIDs(int serversNumber, string[] serverIDs)
    {
        serverIDs = new string[availableServersNum];

        for (int i = serversNumber - 1; i >= 0; i--)
        {
            serverIDs[i] = (i + 1).ToString();
        }
    }
}
