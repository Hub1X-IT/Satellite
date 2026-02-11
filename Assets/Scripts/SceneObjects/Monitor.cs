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
        PowerManager.Instance.OnPowerStateChanged += (isPowerOn) =>
        {
            ComputerComponent.SetComputerEnabled(isPowerOn);
        };
    }
}
