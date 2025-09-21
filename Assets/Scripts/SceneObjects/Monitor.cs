using UnityEngine;

public class Monitor : MonoBehaviour
{
    public Computer ComputerComponent { get; private set; }
    public Desk ParentDesk { get; private set; }

    private void Awake()
    {
        ComputerComponent = GetComponent<Computer>();
        ParentDesk = GetComponentInParent<Desk>();
    }

    private void Start()
    {
        // DetectionManager.DetectionOccured += computer.ExitComputerView;

        // ServerConnectionManager.ServerConnectionEnabled += computer.SetComputerEnabled;
        DetectionManager.ServerPowerEnabled += ComputerComponent.SetComputerEnabled;

        // SetComputerEnabled(true) is called in Awake() in Computer, so this has to be called in Start() in order to disable it
        ComputerComponent.SetComputerEnabled(true);
    }
}
