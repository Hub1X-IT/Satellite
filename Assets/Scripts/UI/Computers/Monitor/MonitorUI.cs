using UnityEngine;
using UnityEngine.UI;

public class MonitorUI : MonoBehaviour
{
    [SerializeField]
    private MonitorUIStartMenu startMenu;

    [SerializeField]
    private Button startMenuButton;

    private void Awake()
    {
        startMenuButton.onClick.AddListener(startMenu.ToggleStartMenu);
    }
}