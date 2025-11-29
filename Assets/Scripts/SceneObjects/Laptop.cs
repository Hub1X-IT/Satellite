using UnityEngine;

public class Laptop : MonoBehaviour
{
    private Computer computer;

    private void Awake()
    {
        computer = GetComponent<Computer>();
    }

    private void Start()
    {
        computer.ComputerViewEnabled += (enabled) =>
        {
            if (enabled)
            {
                GameInput.Instance.CurrentInputActions.CommandPrompt.Enable();
            }
            else
            {
                GameInput.Instance.CurrentInputActions.CommandPrompt.Disable();
            }
        };
    }
}
