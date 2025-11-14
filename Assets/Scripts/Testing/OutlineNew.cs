using UnityEngine;

public class OutlineNew : MonoBehaviour
{
    private const int OutlineRenderingLayerIndex = 2;

    [SerializeField]
    private GameObject targetObject;

    private Renderer[] targetRenderers;

    private void Awake()
    {
        if (targetObject == null)
        {
            Debug.LogError("Outline: target object is not assigned.");
        }

        targetRenderers = targetObject.GetComponentsInChildren<Renderer>(includeInactive: true);
    }

    public void SetOutlineEnabled(bool enabled)
    {
        foreach (var renderer in targetRenderers)
        {
            if (enabled)
            {
                renderer.renderingLayerMask |= (1u << OutlineRenderingLayerIndex);
            }
            else
            {
                renderer.renderingLayerMask &= ~(1u << OutlineRenderingLayerIndex);
            }
        }
    }
}