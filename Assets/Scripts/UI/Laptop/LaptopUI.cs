using UnityEngine;

public class LaptopUI : MonoBehaviour
{
    // [SerializeField]
    private Laptop laptop;

    [SerializeField]
    private CommandPromptUI commandPromptUI;

    private void Awake()
    {
        // There should be only one object with the script Laptop in the scene!
        laptop = FindAnyObjectByType<Laptop>();

        laptop.LaptopViewSetActive += (enabled) =>
        {
            commandPromptUI.enabled = enabled;
        };
    }
}