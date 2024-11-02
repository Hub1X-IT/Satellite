using UnityEngine;

public interface IScreenUIInteractable
{
    public Transform SelfTransform { get; }

    public void SetHighlighted(bool highlighted);

    public void LeftClick();

    public void RightClick();
}
