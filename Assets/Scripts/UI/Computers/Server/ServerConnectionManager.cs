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
        ChangeColors();
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
        ChangeColors();
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
        ChangeColors();
    }

    private void ChangeColors()
    {
        for(int i = 0; i < possibleConnectionItems.Count; i++)
        {
            if (currentConnectedServer == null)
            {
                possibleConnectionItems[i].serverIcon.color = Color.red;
                possibleConnectionItems[i].toggleConnectionButton.image.color = Color.red;
                possibleConnectionItems[i].toggleConnectionButton.interactable = true;
            }
            if (currentConnectedServer != null)
            {
                possibleConnectionItems[i].serverIcon.color = Color.gray;
                possibleConnectionItems[i].toggleConnectionButton.image.color = Color.gray;
                currentConnectedServer.serverIcon.color = Color.green;
                currentConnectedServer.toggleConnectionButton.image.color = Color.green;
                if (currentConnectedServer != possibleConnectionItems[i])
                {
                    possibleConnectionItems[i].toggleConnectionButton.interactable = false;
                }
            }
        }
    }
}