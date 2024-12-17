using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ServerConnectionManager : MonoBehaviour
{
    public static event Action<bool> ServerConnectionEnabled;

    [SerializeField]
    private List<ServerConnectionItem> possibleConnectionItems;

    private ServerConnectionItem currentConnectedServer;

    public static bool IsConnectionActive { get; private set; }

    private void Awake()
    {
        // possibleConnectionItems = GetComponentsInChildren<ServerConnectionItem>().ToList();
        foreach (var serverConnectionItem in possibleConnectionItems)
        {
            serverConnectionItem.ConnectionEnabled += SetCurrentConnectedServer;
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

    private void SetCurrentConnectedServer(ServerConnectionItem serverConnectionItem)
    {
        if(currentConnectedServer == null)
        {
            IsConnectionActive = true;
            currentConnectedServer = serverConnectionItem;
            ServerConnectionEnabled?.Invoke(true);
        }    
    }

    public void DeleteServer(ServerConnectionItem serverConnectionItem)
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
    }
}