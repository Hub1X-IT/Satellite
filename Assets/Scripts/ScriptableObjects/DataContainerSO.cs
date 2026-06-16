using UnityEngine;

public class DataContainerSO : ScriptableObject
{
    [SerializeField]
    private string selfName;

    public string SelfName => selfName;

    public FolderSO ParentFolderSO { get; set; }

    [SerializeField]
    private bool isEncrypted;

    public bool IsEncrypted => isEncrypted;

    public string DataContainerPassword { get; set; }

    public bool IsLocked { get; set; }

    [SerializeField]
    private GameEventSO onTryOpenGameEvent;
    [SerializeField]
    private GameEventSO onOpenGameEvent;

    public virtual void InitializeDataContainerSO()
    {
        IsLocked = IsEncrypted;
    }

    public void RaiseOnTryOpenGameEvent()
    {
        if (onTryOpenGameEvent != null)
        {
            onTryOpenGameEvent.RaiseEvent();
        }
    }

    public void RaiseOnOpenGameEvent()
    {
        if (onOpenGameEvent != null)
        {
            onOpenGameEvent.RaiseEvent();
        }
    }
}
