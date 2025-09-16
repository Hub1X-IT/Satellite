using System;
using Unity.Cinemachine;
using UnityEngine;

public class CameraBobController : MonoBehaviour
{
    [Serializable]
    private class CinemachineBasicMultiChannelPerlinSettings
    {
        public NoiseSettings NoiseProfile;
        public Vector3 PivotOffset;
        public float AmplitudeGain;
        public float FrequencyGain;
    }

    [SerializeField]
    private CinemachineCamera playerCinemachineCamera;

    private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;

    [SerializeField]
    private PlayerMovementController playerMovementController;

    [SerializeField]
    private CinemachineBasicMultiChannelPerlinSettings playerNotMovingSettings;

    [SerializeField]
    private CinemachineBasicMultiChannelPerlinSettings playerMovingSettings;



    private void Awake()
    {
        playerMovementController.StartedMoving += OnPlayerStartedMoving;

        cinemachineBasicMultiChannelPerlin = playerCinemachineCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void OnDestroy()
    {
        playerMovementController.StartedMoving -= OnPlayerStartedMoving;
    }

    public void OnPlayerStartedMoving(bool isMoving)
    {
        if (isMoving)
        {
            cinemachineBasicMultiChannelPerlin.NoiseProfile = playerMovingSettings.NoiseProfile;
            cinemachineBasicMultiChannelPerlin.PivotOffset = playerMovingSettings.PivotOffset;
            cinemachineBasicMultiChannelPerlin.AmplitudeGain = playerMovingSettings.AmplitudeGain;
            cinemachineBasicMultiChannelPerlin.FrequencyGain = playerMovingSettings.FrequencyGain;
        }
        else
        {
            cinemachineBasicMultiChannelPerlin.NoiseProfile = playerNotMovingSettings.NoiseProfile;
            cinemachineBasicMultiChannelPerlin.PivotOffset = playerNotMovingSettings.PivotOffset;
            cinemachineBasicMultiChannelPerlin.AmplitudeGain = playerNotMovingSettings.AmplitudeGain;
            cinemachineBasicMultiChannelPerlin.FrequencyGain = playerNotMovingSettings.FrequencyGain;
        }
    }
}