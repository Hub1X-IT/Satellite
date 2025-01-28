using UnityEngine;

public class LaptopUI : MonoBehaviour
{
    private ComputerUI computerUI;

    [SerializeField]
    private CommandPromptScroll commandPromptScroll;

    private void Awake()
    {
        computerUI = GetComponent<ComputerUI>();
        computerUI.ComputerViewEnabled += (enabled) => {
            commandPromptScroll.enabled = enabled;
        };

        commandPromptScroll.enabled = false;
    }
}
