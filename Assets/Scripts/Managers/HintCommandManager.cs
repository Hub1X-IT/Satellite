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
        commandData.Response?.Invoke(CommandResponseData.Success("Hint command registered (WIP)"));
    }
}
