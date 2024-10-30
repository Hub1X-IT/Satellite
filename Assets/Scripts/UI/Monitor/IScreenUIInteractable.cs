using UnityEngine;

public interface IScreenUIInteractable
{
    public Transform SelfTransform { get; }

    public void Select();

    public void Deselect();

    public void LeftClick();

    public void RightClick();
}
