using UnityEngine;

public class SettingsScriptsInitializer : MonoBehaviour
{
    [SerializeField]
    private VolumeController.InitializationData volumeControllerData;

    private void Awake()
    {
        GraphicsSettingsManager.OnAwake();
        VolumeController.OnAwake(volumeControllerData);

        GameSettingsManager.LoadSettings();
    }

    private void Start()
    {
        GraphicsSettingsManager.OnStart();
        VolumeController.OnStart();
    }

    private void OnDestroy()
    {
        GameSettingsManager.SaveSettings();
    }
}
