using Unity.Cinemachine;
using UnityEngine;

public class Guidebook : MonoBehaviour
{
    private Computer computer;

    private void Awake()
    {
        computer = GetComponent<Computer>();

        computer.ComputerViewEnabled += (enabled) =>
        {
            if (enabled)
            {
                GameInput.PlayerInputActions.Guidebook.Enable();
            }
            else
            {
                GameInput.PlayerInputActions.Guidebook.Disable();
            }
        };
    }
}
