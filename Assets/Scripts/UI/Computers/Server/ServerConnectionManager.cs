using System;
using System.Collections.Generic;
using UnityEngine;

public class ServerConnectionManager : MonoBehaviour
{
    public static ServerConnectionManager Instance { get; private set; }

    public event Action<bool> ServerConnectionEnabled;

    public bool IsConnectionActive { get; private set; }
    public bool WasEverConnected { get; private set; }

    [SerializeField]
    private GameEventSO connectionEnabledGameEvent;

    [SerializeField]
    private List<string> availableServerIDList;

    public string CurrentServerID => availableServerIDList[0];

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

        IsConnectionActive = false;
        WasEverConnected = false;
    }

    private void Start()
    {
        DetectionManager.Instance.DetectionOccured += () =>
        {
            if (IsConnectionActive)
            {
                DeleteCurrentServer();
            }
            else
            {
                Debug.LogWarning("No active server connection!");
            }
        };
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

    private void SetServerConnectionEnabled(bool enabled)
    {
        IsConnectionActive = enabled;
        if (!WasEverConnected && enabled)
        {
            WasEverConnected = true;
        }

        ServerConnectionEnabled?.Invoke(enabled);

        if (enabled && connectionEnabledGameEvent != null)
        {
            connectionEnabledGameEvent.TryRaiseEvent();
        }
    }

    private void DeleteCurrentServer()
    {
        SetServerConnectionEnabled(false);

        if (AvailableServersNumber == 0)
        {
            Debug.LogWarning("No available servers to delete.");
            return;
        }

        availableServerIDList.RemoveAt(0);
    }
}