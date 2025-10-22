using System.Collections.Generic;
using UnityEngine;

public class MonitorFileSystemInitializer : MonoBehaviour
{
    // Can be more than one instance in scene - for different computers

    [SerializeField]
    MonitorUI monitorUI;

    private FolderSO rootFolderSO;

    [SerializeField]
    private PossiblePasswordsSO possiblePasswordsSO;

    [SerializeField]
    private GameEventCommandDataSO connectCommandGameEvent;
    [SerializeField]
    private GameEventCommandDataSO disconnectCommandGameEvent;
    private GameEventSO objective;

    public FolderSO RootFolderSO => rootFolderSO;

    [SerializeField]
    private SerializableDictionary<string, FolderSO> ipAndFolderSerializableDictionary;
    [SerializeField]
    private SerializableDictionary<string, GameEventSO> ipAndObjectiveSerializableDictionary;

    private Dictionary<string, FolderSO> ipAndFolderDictionary;
    private Dictionary<string, GameEventSO> ipAndObjectiveDictionary;


    private string currentIPAddress;

    private void Awake()
    {
        ipAndFolderDictionary = ipAndFolderSerializableDictionary.Dictionary;
        ipAndObjectiveDictionary = ipAndObjectiveSerializableDictionary.Dictionary;

        possiblePasswordsSO.InitializePossiblePasswords();

        connectCommandGameEvent.EventRaised += OnConnectCommand;
        disconnectCommandGameEvent.EventRaised += OnDisconnectCommand;

        monitorUI.FileExplorer.SetFileExplorerEnabled(false);
    }

    private void Start()
    {
        // monitorUI.FileExplorer.InitializeFileExplorer(this);
    }

    private void OnConnectCommand(CommandData commandData)
    {
        if (!monitorUI.IsSputnikOSStarted)
        {
            commandData.Response?.Invoke(false, "Cannot connect to target - SputnikOS is not started.");
            return;
        }

        string ipAddress = commandData.CommandDataArray[0];
        if (rootFolderSO != null)
        {
            commandData.Response?.Invoke(false, $"Already connected to target: {currentIPAddress}. Disconnect first.");
        }
        else if (ipAndFolderDictionary.ContainsKey(ipAddress))
        {
            rootFolderSO = ipAndFolderDictionary[ipAddress];
            monitorUI.FileExplorer.SetFileExplorerEnabled(true);
            monitorUI.FileExplorer.InitializeFileExplorer(this);
            currentIPAddress = ipAddress;
            commandData.Response?.Invoke(true, $"Connected to target: {ipAddress}");
            objective = ipAndObjectiveDictionary[ipAddress];
            objective?.TryRaiseEvent();
        }
        else
        {
            commandData.Response?.Invoke(false, $"{ipAddress} is not available or is not a valid IP address.");
        }
    }

    private void OnDisconnectCommand(CommandData commandData)
    {
        if (rootFolderSO != null)
        {
            monitorUI.FileExplorer.SetFileExplorerEnabled(false);
            rootFolderSO = null;
            string ipAddress = currentIPAddress;
            currentIPAddress = null;
            commandData.Response?.Invoke(true, $"Disconnected successfully from {ipAddress}.");
        }
        else
        {
            commandData.Response?.Invoke(false, "Currently not connected.");
        }
    }
}