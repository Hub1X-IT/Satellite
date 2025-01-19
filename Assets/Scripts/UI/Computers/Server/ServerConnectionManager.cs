using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ServerConnectionManager : MonoBehaviour
{
    public static event Action<bool> ServerConnectionEnabled;

    [SerializeField]
    private List<ServerConnectionItemUI> possibleConnectionItems;

    private ServerConnectionItemUI currentConnectedServer;

    public static bool IsConnectionActive { get; private set; }

    [SerializeField]
    private GameEventSO objectiveGameEvent;

    [SerializeField]
    private Color connectionInactiveColor = Color.red;
    [SerializeField]
    private Color notConnectedColor = Color.gray;
    [SerializeField]
    private Color connectedColor = Color.green;

    private void Awake()
    {
        // possibleConnectionItems = GetComponentsInChildren<ServerConnectionItem>().ToList();
        foreach (var serverConnectionItem in possibleConnectionItems)
        {
            serverConnectionItem.ConnectionEnabled += SetCurrentConnectedServer;
            serverConnectionItem.ConnectionDisabled += DisconnectCurrentServer;
        }
        IsConnectionActive = false;
    }

    private void Start()
    {
        DetectionManager.DetectionOccured += () =>
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
        currentConnectedServer = serverConnectionItem;
        ServerConnectionEnabled?.Invoke(true);
        UpdateConnectionItems();
        if (objectiveGameEvent != null)
        {
            objectiveGameEvent.TryRaiseEvent();
        }
    }

    private void DisconnectCurrentServer()
    {
        IsConnectionActive = false;
        currentConnectedServer = null;
        ServerConnectionEnabled?.Invoke(false);
        UpdateConnectionItems();
    }

    public void DeleteServer(ServerConnectionItemUI serverConnectionItem)
    {
        if (serverConnectionItem == currentConnectedServer)
        {
            IsConnectionActive = false;
            currentConnectedServer.gameObject.SetActive(false);
            // Destroy(currentConnectedServer.gameObject);
            currentConnectedServer = null;
        }
        possibleConnectionItems.Remove(serverConnectionItem);
        ServerConnectionEnabled?.Invoke(false);
        UpdateConnectionItems();
    }

    private void UpdateConnectionItems()
    {
        foreach (var connectionItem in possibleConnectionItems)
        {
            connectionItem.SetColor(IsConnectionActive ? notConnectedColor : connectionInactiveColor);
            connectionItem.SetInteractionEnabled(!IsConnectionActive);
        }
        if (IsConnectionActive)
        {
            currentConnectedServer.SetColor(connectedColor);
            currentConnectedServer.SetInteractionEnabled(true);
        }
    }
}