using UnityEngine;

public class Lightbulb : MonoBehaviour
{
    [SerializeField]
    private Light lightSource;

    [SerializeField]
    private Renderer lightbulbVisualRenderer;

    [SerializeField]
    private Material lightbulbOnMaterial;
    [SerializeField]
    private Material lightbulbOffMaterial;

    private void Start()
    {
        PowerManager.Instance.OnPowerStateChanged += SetLightEnabled;
        SetLightEnabled(true);
    }

    private void SetLightEnabled(bool enabled)
    {
        lightSource.gameObject.SetActive(enabled);
        lightbulbVisualRenderer.material = enabled ? lightbulbOnMaterial : lightbulbOffMaterial;
    }
}
