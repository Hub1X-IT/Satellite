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

    public CommandResponseData()
    {
        ExecutionStatus = CommandExecutionStatus.Unknown;
        ResponseStringArray = null;
    }

    public static CommandResponseData Success(params string[] responseMessages) => new(CommandExecutionStatus.Success, responseMessages);
    public static CommandResponseData Failure(params string[] responseMessages) => new(CommandExecutionStatus.Failure, responseMessages);
    public static CommandResponseData InProgress(params string[] responseMessages) => new(CommandExecutionStatus.InProgress, responseMessages);

    public CommandResponseData(CommandExecutionStatus executionStatus, params string[] responseMessages)
    {
        ExecutionStatus = executionStatus;
        ResponseStringArray = responseMessages;
    }
}