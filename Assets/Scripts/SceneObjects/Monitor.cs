using UnityEngine;

public class Monitor : MonoBehaviour
{
    private Computer computer;

    private void Awake()
    {
        computer = GetComponent<Computer>();

        computer.ComputerViewEnabled += (enabled) =>
        {
            if (enabled)
            {
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
                GameManager.SetCursorShown(false);
            }
        };
    }
}
