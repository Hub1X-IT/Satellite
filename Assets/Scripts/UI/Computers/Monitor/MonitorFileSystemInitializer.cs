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
        else if (ipAndFolderDictionary.ContainsKey(ipAddress))
        {
            rootFolderSO = ipAndFolderDictionary[ipAddress];
            monitorUI.FileExplorer.SetFileExplorerEnabled(true);
            monitorUI.FileExplorer.InitializeFileExplorer(this);
            currentIPAddress = ipAddress;
            responseData = CommandResponseData.Success($"Connected to target: {ipAddress}");
            if (ipAndObjectiveDictionary.ContainsKey(ipAddress))
            {
                objective = ipAndObjectiveDictionary[ipAddress];
                objective.RaiseEvent();
            }
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

        if (rootFolderSO != null)
        {
            monitorUI.FileExplorer.SetFileExplorerEnabled(false);
            rootFolderSO = null;
            string ipAddress = currentIPAddress;
            currentIPAddress = null;
            responseData = CommandResponseData.Success($"Disconnected successfully from {ipAddress}.");
        }
        else
        {
            responseData = CommandResponseData.Failure("Currently not connected.");
        }

        commandData.Response?.Invoke(responseData);
    }
}