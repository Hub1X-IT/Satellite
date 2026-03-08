using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ServerConnectionUI : MonoBehaviour
{
    [SerializeField]
    private Button toggleConnectionButton;

    [SerializeField]
    private TMP_Dropdown availableServersDropdown;

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

    private List<string> availableServerIDList;

    private bool isConnectionActive;

    private void Awake()
    {
        connectionStatusTextField.text = DisconnectedStatusText;
        serverIDTextField.text = NullServerIDText;
        isConnectionActive = false;
    }

    private void Start()
    {
        toggleConnectionButton.onClick.AddListener(OnToggleConnectionButtonClicked);

        ServerConnectionManager.Instance.OnServerConnectionStateChanged += UpdateServerConnectionUI;

        UpdateServerConnectionUI(ServerConnectionManager.Instance.IsConnectionActive);
    }

    private void OnToggleConnectionButtonClicked()
    {
        if (isConnectionActive)
        {
            ServerConnectionManager.Instance.TryDisconnectFromServer();
        }
        else
        {
            ServerConnectionManager.Instance.TryConnectToServer(availableServerIDList[availableServersDropdown.value]);
        }
    }

    private void UpdateServerConnectionUI(bool isConnected)
    {
        isConnectionActive = isConnected;

        connectionStatusTextField.text = isConnected ? ConnectedStatusText : DisconnectedStatusText;
        connectionButtonTextField.text = isConnected ? DisconnectText : ConnectText;
        availableServersTextField.text = ServerConnectionManager.Instance.AvailableServersNumber.ToString();
        serverIDTextField.text = isConnected ? ServerConnectionManager.Instance.CurrentServerSO.ServerID : NullServerIDText;

        connectionStatusTextField.color = isConnected ? connectedColor : textDisconnectedColor;
        serverIcon.color = isConnected ? connectedColor : serverIconDisconnectedColor;
        toggleConnectionButton.image.color = isConnected ? buttonDisconnectColor : connectedColor;

        RefreshDropdownOptions();
    }

    private void RefreshDropdownOptions()
    {
        availableServerIDList = new();

        int currentDropdownValue = 0;

        for(int i = 0; i < ServerConnectionManager.Instance.AvailableServersList.Count; i++)
        {
            ServerSO serverSO = ServerConnectionManager.Instance.AvailableServersList[i];
            availableServerIDList.Add(serverSO.ServerID);

            if (serverSO == ServerConnectionManager.Instance.CurrentServerSO)
            {
                currentDropdownValue = i;
            }
        }

        availableServersDropdown.ClearOptions();
        availableServersDropdown.AddOptions(availableServerIDList);
        availableServersDropdown.value = currentDropdownValue;
    }
}
