using UnityEngine;

public class HintCommandManager : MonoBehaviour
{
    [SerializeField]
    private GameEventCommandDataSO hintCommandGameEvent;

    private void Awake()
    {
        hintCommandGameEvent.EventRaised += OnHintCommand;
    }

    private void OnHintCommand(CommandData commandData)
    {
        commandData.Response?.Invoke(new CommandResponseData
        {
            ExecutionStatus = CommandExecutionStatus.Success,
            ResponseStringArray = new string[] { "Hint command registered (WIP)" }
        });
    }
}
