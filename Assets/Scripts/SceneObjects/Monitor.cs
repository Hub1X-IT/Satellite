using UnityEngine;

public class Monitor : MonoBehaviour
{
    private Computer computer;

    private void Awake()
    {
        computer = GetComponent<Computer>();
    }
    

    private void Start()
    {
        DetectionManager.DetectionOccured += computer.ExitComputerView;

        ServerConnectionManager.ServerConnectionEnabled += computer.SetComputerEnabled;
        DetectionManager.ServerPowerEnabled += computer.SetComputerEnabled;

        // SetComputerEnabled(true) is called in Awake() in Computer, so this has to be called in Start() in order to disable it
        computer.SetComputerEnabled(false);
    }
}
