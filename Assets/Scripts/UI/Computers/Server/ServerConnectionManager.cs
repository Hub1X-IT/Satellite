using System;
using System.Collections.Generic;
using UnityEngine;

public class ServerConnectionManager : MonoBehaviour
{
    public static ServerConnectionManager Instance { get; private set; }

    public event Action<bool> ServerConnectionEnabled;

    [SerializeField]
    private ServerConnectionItemUI connectionItemUI;

    private ServerConnectionItemUI currentConnectedServer;

    public bool IsConnectionActive { get; private set; }
    public bool WasEverConnected { get; private set; }

    [SerializeField]
    private GameEventSO connectionEnabledGameEvent;

    [SerializeField]
    private Color connectionInactiveColor = Color.red;
    [SerializeField]
    private Color notConnectedColor = Color.gray;
    [SerializeField]
    private Color connectionActiveColor = Color.green;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning($"Multiple {nameof(ServerConnectionManager)} instances detected! Destroying duplicate.");
            Destroy(gameObject);
            return;
        }
        Instance = this;


        connectionItemUI.ConnectionEnabled += SetCurrentConnectedServer;
        connectionItemUI.ConnectionDisabled += DisconnectCurrentServer;
        IsConnectionActive = false;
        WasEverConnected = false;
    }

    private void Start()
    {
        DetectionManager.Instance.DetectionOccured += () =>
        {
            if (currentConnectedServer != null)
            {
                DeleteServer(currentConnectedServer);
            }
        };
    }

    private void OnDestroy()
    {
        ServerConnectionEnabled = null;
    }

    private void SetCurrentConnectedServer(ServerConnectionItemUI serverConnectionItem)
    {
        IsConnectionActive = true;
        WasEverConnected = true;
        currentConnectedServer = serverConnectionItem;
        ServerConnectionEnabled?.Invoke(true);
        UpdateConnectionColor();
        if (connectionEnabledGameEvent != null)
        {
            connectionEnabledGameEvent.TryRaiseEvent();
        }
    }

    private void DisconnectCurrentServer()
    {
        IsConnectionActive = false;
        currentConnectedServer = null;
        ServerConnectionEnabled?.Invoke(false);
        UpdateConnectionColor();
    }

    private void DeleteServer(ServerConnectionItemUI serverConnectionItem)
    {
        if (serverConnectionItem == currentConnectedServer)
        {
            IsConnectionActive = false;
            currentConnectedServer.gameObject.SetActive(false);
            // Destroy(currentConnectedServer.gameObject);
            currentConnectedServer = null;
        }
        ServerConnectionEnabled?.Invoke(false);
        UpdateConnectionColor();
        connectionItemUI.availableServersNum--;
    }

    private void UpdateConnectionColor()
    {

        connectionItemUI.SetColor(IsConnectionActive ? connectionInactiveColor : connectionActiveColor);
        //connectionItemUI.SetInteractionEnabled(!IsConnectionActive);

        /*if (IsConnectionActive)
        {
            currentConnectedServer.SetColor(connectedColor);
            currentConnectedServer.SetInteractionEnabled(true);
        }*/
    }
}