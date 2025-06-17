using UnityEngine;

public class LaptopUI : MonoBehaviour
{
    private ComputerUI computerUI;

    [SerializeField]
    private CommandPromptScroll commandPromptScroll;

    [SerializeField]
    private CommandPromptUI commandPromptUI;

    private void Awake()
    {
        computerUI = GetComponent<ComputerUI>();
        computerUI.ComputerViewEnabled += (enabled) =>
        {
            commandPromptScroll.enabled = enabled;
            if (enabled)
            {
                commandPromptUI.FocusOnInputField();
            }
        };

        commandPromptScroll.enabled = false;
    }
}
