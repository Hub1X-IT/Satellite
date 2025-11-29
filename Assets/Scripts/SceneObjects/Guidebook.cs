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
    }

    private void Start()
    {
        computer.ComputerViewEnabled += (enabled) =>
        {
            if (enabled)
            {
                GameInput.Instance.CurrentInputActions.Guidebook.Enable();
            }
            else
            {
                GameInput.Instance.CurrentInputActions.Guidebook.Disable();
            }
        };
    }
}
