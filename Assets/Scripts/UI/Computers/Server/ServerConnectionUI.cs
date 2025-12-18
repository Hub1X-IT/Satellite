using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ServerConnectionUI : MonoBehaviour
{
    [SerializeField]
    private Button toggleConnectionButton;

    [SerializeField]
    private Image serverIcon;

    [SerializeField]
    private TMP_Text connectionButtonTextField;
    [SerializeField]
    private TMP_Text connectionStatusTextField;
    [SerializeField]
    private TMP_Text availableServersTextField;
    [SerializeField]
    private TMP_Text serverIDTextField;

    [SerializeField]
    private Color buttonDisconnectColor = Color.red;
    [SerializeField]
    private Color serverDisconnectedColor = Color.cyan;
    [SerializeField]
    private Color connectedColor = Color.green;

    private const string disconnectedStatus = "<color=\"red\">Disconnected</color>";
    private const string connectedStatus = "<color=\"green\">Connected</color>";
    private const string nullServerID = "NULL";

    private const string DisconnectText = "Disconnect";
    private const string ConnectText = "Connect";

    private void Awake()
    {
        connectionStatusTextField.text = disconnectedStatus;
        serverIDTextField.text = nullServerID;
    }

    private void Start()
    {
        toggleConnectionButton.onClick.AddListener(ServerConnectionManager.Instance.TryToggleServerConnection);
        availableServersTextField.text = ServerConnectionManager.Instance.AvailableServersNumber.ToString();

        ServerConnectionManager.Instance.ServerConnectionEnabled += OnServerConnectionEnabled;

        toggleConnectionButton.image.color = connectedColor;
        serverIcon.color = serverDisconnectedColor;
    }

    private void OnServerConnectionEnabled(bool enabled)
    {
        connectionStatusTextField.text = enabled ? connectedStatus : disconnectedStatus;
        connectionButtonTextField.text = enabled ? DisconnectText : ConnectText;
        availableServersTextField.text = ServerConnectionManager.Instance.AvailableServersNumber.ToString();
        serverIDTextField.text = enabled ? ServerConnectionManager.Instance.CurrentServerID : nullServerID;
        serverIcon.color = enabled ? connectedColor : serverDisconnectedColor;
        toggleConnectionButton.image.color = enabled ? buttonDisconnectColor : connectedColor;
    }
}
