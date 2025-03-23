using System;

public class CommandData
{
    public string[] CommandDataArray { get; set; }
    public Action<bool, string> Response;
}