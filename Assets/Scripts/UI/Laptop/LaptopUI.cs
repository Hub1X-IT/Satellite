using UnityEngine;

public class LaptopUI : MonoBehaviour
{
    private ComputerUI computerUI;

    [SerializeField]
    private CommandPromptUI commandPromptUI;

    private void Awake()
    {
        computerUI = GetComponent<ComputerUI>();

        computerUI.ComputerViewEnabled += (enabled) =>
        {
            commandPromptUI.enabled = enabled;
        };
    }
}