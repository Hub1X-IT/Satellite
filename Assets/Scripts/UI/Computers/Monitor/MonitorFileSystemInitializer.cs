using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonitorFileSystemInitializer : MonoBehaviour
{
    [Serializable]
    private class IpData
    {
        public string IpAddress;
        public FolderSO FolderSO;
        public GameEventSO OnConnectGameEvent;
    }

    // Can be more than one instance in scene - for different computers

    [SerializeField]
    private MonitorUI monitorUI;

    private FolderSO rootFolderSO;

    [SerializeField]
    private PossiblePasswordsSO possiblePasswordsSO;

    [SerializeField]
    private GameEventCommandDataSO connectCommandGameEvent;
    [SerializeField]
    private GameEventCommandDataSO disconnectCommandGameEvent;

    public FolderSO RootFolderSO => rootFolderSO;

    [SerializeField]
    private IpData[] ipDataInitialArray;

    private Dictionary<string, IpData> ipDataDictionary;

    private readonly MonitorAppsManagerUI.ApplicationType[] appsToCloseOnDisconnect = {
        MonitorAppsManagerUI.ApplicationType.DataContainerPasswordScreen,
        MonitorAppsManagerUI.ApplicationType.NotepadApp,
        MonitorAppsManagerUI.ApplicationType.DoorApp
    };

    private string currentIPAddress;

    private void Awake()
    {
        ipDataDictionary = ipDataInitialArray.ToDictionary(keySelector: item => item.IpAddress, elementSelector: item => item);

        possiblePasswordsSO.InitializePossiblePasswords();

        connectCommandGameEvent.EventRaised += OnConnectCommand;
        disconnectCommandGameEvent.EventRaised += OnDisconnectCommand;

        monitorUI.FileExplorer.SetFileExplorerEnabled(false);

        ServerConnectionManager.Instance.OnServerConnectionStateChanged += (isConnected) =>
        {
            if (!isConnected) TryDisconnect();
        };
    }

    private void OnConnectCommand(CommandData commandData)
    {
        if (!monitorUI.IsSputnikOSStarted)
        {
            commandData.Response?.Invoke(CommandResponseData.Failure("Cannot connect to target - SputnikOS is not started."));
            return;
        }

        string ipAddress = commandData.CommandDataArray[0];

        CommandResponseData responseData;

        if (rootFolderSO != null)
        {
            responseData = CommandResponseData.Failure($"Already connected to target: {currentIPAddress}. Disconnect first.");
        }
        else if (TryConnectTo(ipAddress))
        {
            responseData = CommandResponseData.Success($"Connected to target: {ipAddress}");
        }
        else
        {
            responseData = CommandResponseData.Failure($"{ipAddress} is not available or is not a valid IP address.");
        }

        commandData.Response?.Invoke(responseData);
    }

    private void OnDisconnectCommand(CommandData commandData)
    {
        CommandResponseData responseData;

        string ipAddress = currentIPAddress;
        if (TryDisconnect())
        {
            responseData = CommandResponseData.Success($"Disconnected successfully from {ipAddress}.");
        }
        else
        {
            responseData = CommandResponseData.Failure("Currently not connected.");
        }

        commandData.Response?.Invoke(responseData);
    }

    public bool TryConnectTo(string ipAddress)
    {
        if (!ipDataDictionary.TryGetValue(ipAddress, out var ipData)) return false;

        if (rootFolderSO != null)
        {
            Debug.LogWarning($"Connecting to {ipAddress} while still connected to {currentIPAddress}.");
            TryDisconnect();
        }

        rootFolderSO = ipData.FolderSO;
        monitorUI.FileExplorer.SetFileExplorerEnabled(true);
        monitorUI.FileExplorer.InitializeFileExplorer(this);
        currentIPAddress = ipAddress;

        if (ipData.OnConnectGameEvent != null)
        {
            ipData.OnConnectGameEvent.RaiseEvent();
        }

        return true;
    }

    public bool TryDisconnect()
    {
        if (rootFolderSO == null) return false;

        monitorUI.FileExplorer.SetFileExplorerEnabled(false);

        foreach (var appType in appsToCloseOnDisconnect)
        {
            monitorUI.AppsManager.TryCloseApp(appType);
        }

        rootFolderSO = null;
        currentIPAddress = null;

        return true;
    }
}