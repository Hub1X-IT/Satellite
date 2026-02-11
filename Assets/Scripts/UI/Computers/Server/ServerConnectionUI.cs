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
    private Color serverIconDisconnectedColor = Color.cyan;
    [SerializeField]
    private Color textDisconnectedColor = Color.red;
    [SerializeField]
    private Color connectedColor = Color.green;

    private const string DisconnectedStatusText = "Disconnected";
    private const string ConnectedStatusText = "Connected";
    private const string NullServerIDText = "NULL";

    private const string DisconnectText = "Disconnect";
    private const string ConnectText = "Connect";

    private void Awake()
    {
        connectionStatusTextField.text = DisconnectedStatusText;
        serverIDTextField.text = NullServerIDText;
    }

    private void Start()
    {
        toggleConnectionButton.onClick.AddListener(ServerConnectionManager.Instance.TryToggleServerConnection);

        ServerConnectionManager.Instance.OnServerConnectionStateChanged += UpdateServerConnectionUI;    

        UpdateServerConnectionUI(ServerConnectionManager.Instance.IsConnectionActive);
    }

    private void UpdateServerConnectionUI(bool isConnectionActive)
    {
        connectionStatusTextField.text = isConnectionActive ? ConnectedStatusText : DisconnectedStatusText;
        connectionButtonTextField.text = isConnectionActive ? DisconnectText : ConnectText;
        availableServersTextField.text = ServerConnectionManager.Instance.AvailableServersNumber.ToString();
        serverIDTextField.text = isConnectionActive ? ServerConnectionManager.Instance.CurrentServerID : NullServerIDText;
        
        connectionStatusTextField.color = isConnectionActive ? connectedColor : textDisconnectedColor;
        serverIcon.color = isConnectionActive ? connectedColor : serverIconDisconnectedColor;
        toggleConnectionButton.image.color = isConnectionActive ? buttonDisconnectColor : connectedColor;
    }
}
