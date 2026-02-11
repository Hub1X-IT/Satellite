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
    private List<string> availableServerIDList;

    public string CurrentServerID { get; private set; }
    public int AvailableServersNumber => availableServerIDList.Count;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning($"Multiple {nameof(ServerConnectionManager)} instances detected! Destroying duplicate.");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        CurrentServerID = availableServerIDList[0];

        IsConnectionActive = false;
        WasEverConnected = false;
    }

    public void TryToggleServerConnection()
    {
        if (AvailableServersNumber > 0)
        {
            SetServerConnectionEnabled(!IsConnectionActive);
        }
        else
        {
            Debug.Log("No available servers.");
        }
    }

    public void TryDeleteCurrentServer()
    {
        if (IsConnectionActive)
        {
            DeleteCurrentServer();
        }
        else
        {
            Debug.LogWarning("Tracing ended when no active server connection!");
        }
    }

    private void SetServerConnectionEnabled(bool enabled)
    {
        IsConnectionActive = enabled;
        if (!WasEverConnected && enabled)
        {
            WasEverConnected = true;
        }

        if (enabled)
        {
            CurrentServerID = availableServerIDList[0];
        }

        OnServerConnectionStateChanged?.Invoke(enabled);

        if (enabled && connectionEnabledGameEvent != null)
        {
            connectionEnabledGameEvent.TryRaiseEvent();
        }
    }

    private void DeleteCurrentServer()
    {
        if (AvailableServersNumber == 0)
        {
            Debug.LogWarning("No available servers to delete.");
            return;
        }

        availableServerIDList.Remove(CurrentServerID);
        CurrentServerID = null;

        IsConnectionActive = false;
        OnServerConnectionStateChanged?.Invoke(false);
    }
}