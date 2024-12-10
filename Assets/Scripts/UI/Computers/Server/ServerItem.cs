using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ServerItem : MonoBehaviour
{
    private DetectionManager detectionManager;
    private Desk desk;

    [SerializeField]
    private Button connectButton;

    [SerializeField]
    TMP_Text buttonText;

    private bool disconnected = true;

    private const string DISCONNECT = "Disconnect";
    private const string CONNECT = "Connect";

    private void Awake()
    {
        detectionManager = FindAnyObjectByType<DetectionManager>();
        desk = FindAnyObjectByType<Desk>();
        connectButton.onClick.AddListener(() =>
        {
            if (disconnected)
            {
                buttonText.text = DISCONNECT;
                disconnected = false;
                detectionManager.currentServer = connectButton.gameObject;
                desk.ShouldEnableDeskTrigger = true;
                desk.ToggleDeskTrigger();
            }
            else
            {
                buttonText.text = CONNECT;
                disconnected = true;
                detectionManager.currentServer = null;
                desk.ShouldEnableDeskTrigger = false;
                desk.ToggleDeskTrigger();
            }
        });
    }
}
