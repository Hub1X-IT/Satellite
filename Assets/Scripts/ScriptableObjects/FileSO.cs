using UnityEngine;

public abstract class FileSO : DataContainerSO 
{
    [SerializeField]
    private GameEventSO onOpenFileGameEvent;

    public void TriggerOnOpenEvent()
    {
        if (onOpenFileGameEvent != null)
        {
            onOpenFileGameEvent.RaiseEvent();
        }
    }
}
