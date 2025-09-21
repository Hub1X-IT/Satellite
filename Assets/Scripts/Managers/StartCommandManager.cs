using System;
using UnityEngine;

public class StartCommandManager : MonoBehaviour
{
    [Serializable]
    public class StartCommandProgram
    {
        public string ProgramName;
        public GameEventStartProgramDataSO StartProgramGameEvent;
        public GameEventSO ObjectiveGameEvent;
    }

    [SerializeField]
    GameEventCommandDataSO startCommandGameEvent;

    [SerializeField]
    StartCommandProgram[] startCommandPrograms;


    private void Awake()
    {
        startCommandGameEvent.EventRaised += OnStartCommand;
    }

    private void OnDestroy()
    {
        foreach (var startCommandProgram in startCommandPrograms)
        {
            startCommandProgram.StartProgramGameEvent.ResetGameEvent();
        }
    }

    private void OnStartCommand(CommandData commandData)
    {
        string programName = commandData.CommandDataArray[0];
        foreach (var startCommandProgram in startCommandPrograms)
        {
            if (startCommandProgram.ProgramName == programName)
            {
                startCommandProgram.StartProgramGameEvent.RaiseEvent(new StartProgramEventData
                {
                    Response = (wasSuccessful, responseOutput) => commandData.Response?.Invoke(wasSuccessful, responseOutput),
                });
                if (startCommandProgram.ObjectiveGameEvent != null)
                {
                    startCommandProgram.ObjectiveGameEvent.TryRaiseEvent();
                }
                return;
            }
        }

        commandData.Response?.Invoke(false, $"No program with name: {programName}");
    }
}