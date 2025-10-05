using UnityEngine;

public class Laptop : MonoBehaviour
{
    private Computer computer;

    private void Awake()
    {
        computer = GetComponent<Computer>();

        computer.ComputerViewEnabled += (enabled) =>
        {
            if (enabled)
            {
                GameInput.CurrentInputActions.CommandPrompt.Enable();
            }
            else
            {
                GameInput.CurrentInputActions.CommandPrompt.Disable();
            }
        };
    }
}
