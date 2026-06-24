using UnityEngine;

public class Lightbulb : MonoBehaviour
{
    [SerializeField]
    private GameObject lightSource;

    [SerializeField]
    private Material lightbulbMaterial;

    private float originalEmissiveIntensity;

    private Color emissiveOffColor = Color.black;
    private Color emissiveOnColor;

    private void Start()
    {
        originalEmissiveIntensity = lightbulbMaterial.GetFloat("_EmissiveIntensity");
        emissiveOnColor = lightbulbMaterial.GetColor("_EmissiveColor");
        PowerManager.Instance.OnPowerStateChanged += SetLightEnabled;
        SetLightEnabled(true); //temporary fix to why bloom is not applied at the start and only after lever flip
    }

    private void SetLightEnabled(bool enabled)
    {
        lightSource.SetActive(enabled);
        if (enabled)
        {
            lightbulbMaterial.SetColor("_EmissiveColor", emissiveOnColor * originalEmissiveIntensity);
        }
        else
        {
            lightbulbMaterial.SetColor("_EmissiveColor", emissiveOffColor * 0f);
        }
    }
}
