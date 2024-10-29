using UnityEngine;

public class ScriptsInitializer : MonoBehaviour
{
    [SerializeField]
    private CameraController.InitializationData cameraControllerData;

    [SerializeField]
    private InteractionController.InitializationData interactionControllerData;

    [SerializeField]
    private VolumeController.InitializationData volumeControllerData;


    private void Awake()
    {
        GameInput.InitializeInput();

        CameraController.InitializeOnAwake(cameraControllerData);
        GameManager.InitializeOnAwake();
        InteractionController.InitializeOnAwake(interactionControllerData);
        VolumeController.InitializeOnAwake(volumeControllerData);

        GameSettingsManager.LoadSettings();
    }

    private void Start()
    {
        CommandPromptManager.InitializeOnStart();
        GameManager.InitializeOnStart();
        VolumeController.InitializeOnStart();
    }

    private void OnDestroy()
    {
        GameSettingsManager.SaveSettings();
        GameInput.RemoveInput();
    }
}
