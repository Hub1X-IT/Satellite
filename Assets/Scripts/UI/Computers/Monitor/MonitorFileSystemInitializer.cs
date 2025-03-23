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

    public FolderSO RootFolderSO => rootFolderSO;

    [SerializeField]
    private SerializableDictionary<string, FolderSO> ipAndFolderSerializableDictionary;

    private Dictionary<string, FolderSO> ipAndFolderDictionary;

    private void Awake()
    {
        ipAndFolderDictionary = ipAndFolderSerializableDictionary.Dictionary;

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
        string ipAddress = commandData.CommandDataArray[0];
        if (ipAndFolderDictionary.ContainsKey(ipAddress))
        {
            rootFolderSO = ipAndFolderDictionary[ipAddress];
            monitorUI.FileExplorer.SetFileExplorerEnabled(true);
            monitorUI.FileExplorer.InitializeFileExplorer(this);
            commandData.Response?.Invoke(true, $"Connected to: {ipAddress}");
        }
        else
        {
            commandData.Response?.Invoke(false, $"Failed to connect to: {ipAddress}");
        }
    }

    private void OnDisconnectCommand(CommandData commandData)
    {
        if (rootFolderSO != null)
        {
            monitorUI.FileExplorer.SetFileExplorerEnabled(false);
            rootFolderSO = null;
            commandData.Response?.Invoke(true, "Disconnected successfully.");
        }
        else
        {
            commandData.Response?.Invoke(false, "Wasn't connected");
        }
    }
}