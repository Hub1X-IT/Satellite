public enum CommandExecutionStatus
{
    Unknown,
    Success,
    Failure,
    InProgress,
}

public class CommandResponseData
{
    public CommandExecutionStatus ExecutionStatus;
    public string[] ResponseStringArray;
}