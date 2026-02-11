using UnityEngine;

public class Lightbulb : MonoBehaviour
{
    [SerializeField]
    private GameObject lightSource;

    [SerializeField]
    private Material lightbulbMaterial;

    private void Start()
    {
        PowerManager.Instance.OnPowerStateChanged += SetLightEnabled;
    }

    private void SetLightEnabled(bool enabled)
    {
        lightSource.SetActive(enabled);
        if (enabled)
        {
            lightbulbMaterial.EnableKeyword("_EMISSION");
        }
        else
        {
            lightbulbMaterial.DisableKeyword("_EMISSION");
        }
    }
}
