using UnityEngine;

public class Monitor : MonoBehaviour
{
    private Computer computer;

    private void Awake()
    {
        computer = GetComponent<Computer>();

        computer.ComputerViewEnabled += (enabled) =>
        {
            
        };
    }
}
