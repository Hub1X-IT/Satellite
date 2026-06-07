using UnityEngine;

public class HelpCommandManager : MonoBehaviour
{
    [SerializeField]
    private GameEventCommandDataSO helpCommandGameEvent;

    private readonly string[] helpCommandResponse = { "SputnikGate, version 0.1.2",
    "",
    "help                             display this info",
    "start [program name]             start program",
    "connect [target IP]              connect to target" };


    private void Awake()
    {
        helpCommandGameEvent.EventRaised += OnHelpCommand;
    }

    private void OnHelpCommand(CommandData commandData)
    {
        commandData.Response?.Invoke(new CommandResponseData(CommandExecutionStatus.Success, helpCommandResponse));
    }
}
