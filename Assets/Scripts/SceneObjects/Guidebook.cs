using UnityEngine;

public class Guidebook : MonoBehaviour
{
    private Computer computer;

    [SerializeField]
    private GuidebookInterfaceUI guidebookInterface;

    public GuidebookInterfaceUI GuidebookInterface => guidebookInterface;

    public Computer ComputerComponent => computer;

    private void Awake()
    {
        computer = GetComponent<Computer>();

        computer.ComputerViewEnabled += (enabled) =>
        {
            if (enabled)
            {
                GameInput.CurrentInputActions.Guidebook.Enable();
            }
            else
            {
                GameInput.CurrentInputActions.Guidebook.Disable();
            }
        };
    }
}
