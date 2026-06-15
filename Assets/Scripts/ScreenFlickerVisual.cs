using DG.Tweening;
using UnityEngine;

public class ScreenFlickerVisual : MonoBehaviour
{
    [SerializeField]
    private Material screenMaterial;
    [SerializeField]
    private Light screenLight;

    [SerializeField]
    private float minFlickerDuration = 0.05f;
    [SerializeField]
    private float maxFlickerDuration = 0.2f;
    [SerializeField]
    private float minLightIntensity = 0.5f;
    [SerializeField]
    private float maxLightIntensity = 1.5f;
    [SerializeField]
    private float minEmissionIntensity = 0.5f;
    [SerializeField]
    private float maxEmissionIntensity = 2f;
    [SerializeField]
    private float pauseBetweenFlickers = 0.1f;

    private Sequence screenFlickerSequence;
    private float originalLightIntensity;
    private float originalEmissionIntensity;

    void Start()
    {
        if (screenLight == null)
            screenLight = GetComponent<Light>();

        originalLightIntensity = screenLight.intensity;
        
        if (screenMaterial != null)
            originalEmissionIntensity = screenMaterial.GetFloat("_EmissiveIntensity");

        StartFlickerLoop();
    }

    void StartFlickerLoop()
    {
        if (screenFlickerSequence != null)
            screenFlickerSequence.Kill();

        screenFlickerSequence = DOTween.Sequence();
        screenFlickerSequence.AppendCallback(() => PlayRandomFlicker());
        screenFlickerSequence.AppendInterval(Random.Range(minFlickerDuration, maxFlickerDuration) + pauseBetweenFlickers);
        screenFlickerSequence.SetLoops(-1);
    }

    void PlayRandomFlicker()
    {
        float flickerDuration = Random.Range(minFlickerDuration, maxFlickerDuration);
        float targetLightIntensity = Random.Range(minLightIntensity, maxLightIntensity);
        float targetEmissionIntensity = Random.Range(minEmissionIntensity, maxEmissionIntensity);

        Sequence flicker = DOTween.Sequence();

        // Animate light intensity
        flicker.Append(screenLight.DOIntensity(targetLightIntensity, flickerDuration));

        // Animate material emission if available
        if (screenMaterial != null)
        {
            flicker.Join(DOTween.To(
                () => screenMaterial.GetFloat("_EmissiveIntensity"),
                x => screenMaterial.SetFloat("_EmissiveIntensity", x),
                targetEmissionIntensity,
                flickerDuration
            ));
        }

        // Return to original state
        flicker.Append(screenLight.DOIntensity(originalLightIntensity, flickerDuration * 0.5f));
        if (screenMaterial != null)
        {
            flicker.Join(DOTween.To(
                () => screenMaterial.GetFloat("_EmissiveIntensity"),
                x => screenMaterial.SetFloat("_EmissiveIntensity", x),
                originalEmissionIntensity,
                flickerDuration * 0.5f
            ));
        }
    }

    void OnDestroy()
    {
        if (screenFlickerSequence != null)
            screenFlickerSequence.Kill();
    }
}
