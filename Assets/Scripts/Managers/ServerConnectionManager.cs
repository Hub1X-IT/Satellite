using System;
using System.Collections.Generic;
using UnityEngine;

public class ServerConnectionManager : MonoBehaviour
{
    public static ServerConnectionManager Instance { get; private set; }

    public event Action<bool> OnServerConnectionStateChanged;

    public bool IsConnectionActive { get; private set; }
    public bool WasEverConnected { get; private set; }

    [SerializeField]
    private GameEventSO connectionEnabledGameEvent;

    [SerializeField]
    private List<ServerSO> availableServersList;

    public List<ServerSO> AvailableServersList => availableServersList;

    public ServerSO CurrentServerSO { get; private set; }

    public int AvailableServersNumber => AvailableServersList.Count;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning($"Multiple {nameof(ServerConnectionManager)} instances detected! Destroying duplicate.");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        IsConnectionActive = false;
        WasEverConnected = false;
    }

    public bool TryConnectToServer(string serverID)
    {
        if (!IsConnectionActive)
        {
            foreach (var serverSO in availableServersList)
            {
                if (serverSO.ServerID == serverID)
                {
                    ConnectToServer(serverSO);
                    return true;
                }
            }
        }

        return false;
    }

    public bool TryDisconnectFromServer()
    {
        if (IsConnectionActive)
        {
            DisconnectServer();
            return true;
        }

        return false;
    }

    public bool TryDeleteCurrentServer()
    {
        if (IsConnectionActive)
        {
            availableServersList.Remove(CurrentServerSO);
            DisconnectServer();
            return true;
        }

        return false;
    }

    private void DisconnectServer()
    {
        IsConnectionActive = false;
        CurrentServerSO = null;
        OnServerConnectionStateChanged?.Invoke(false);
    }

    private void ConnectToServer(ServerSO serverSO)
    {
        IsConnectionActive = true;
        if (!WasEverConnected)
        {
            WasEverConnected = true;
        }

        CurrentServerSO = serverSO;

        OnServerConnectionStateChanged?.Invoke(true);

        if (connectionEnabledGameEvent != null)
        {
            connectionEnabledGameEvent.RaiseEvent();
        }
    }
}