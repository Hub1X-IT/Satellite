using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ServerItem : MonoBehaviour
{
    private DetectionManager detectionManager;
    private Desk desk;

    [SerializeField]
    private Button toggleConnectionButton;

    [SerializeField]
    private TMP_Text toggleConnectionButtonText;

    private bool isConnected = true;

    private const string DisconnectText = "Disconnect";
    private const string ConnectText = "Connect";

    private void Awake()
    {
        detectionManager = FindAnyObjectByType<DetectionManager>();
        desk = FindAnyObjectByType<Desk>();
        toggleConnectionButton.onClick.AddListener(() =>
        {
            SetConnectionEnabled(!isConnected);
        });
    }

    private void SetConnectionEnabled(bool enabled)
    {
        isConnected = enabled;
        toggleConnectionButtonText.text = enabled ? DisconnectText : ConnectText;
        desk.SetAllComputersEnabled(enabled);
        detectionManager.currentServer = enabled ? toggleConnectionButton.gameObject : null;
    }
}
