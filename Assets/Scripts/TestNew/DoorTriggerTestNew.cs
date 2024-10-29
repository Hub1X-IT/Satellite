using System;

public class DoorTriggerTestNew : InteractableObjectTestNew
{
    public event Action DoorTriggered;

    public override void Interact()
    {
        base.Interact();
        DoorTriggered?.Invoke();
    }
}
