using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DevTools : MonoBehaviour
{
    public interface IDevAware
    {
        public void EnableDevMode();
        public void DisableDevMode();
    }

    // There's no input sanitization currently, as Unity doesn't normally support interfaces as fields (even though they are serializable).
    // Possible workaround: https://bitbucket.org/gaello/interface-in-inspector/src/master/
    [SerializeField]
    private List<MonoBehaviour> devCompnents;

    [SerializeField]
    private InputAction toggleAction;

    private static bool devEnabled = false;

    private void Awake()
    {
        if (devEnabled) EnableDevMode();
        toggleAction.performed += ToggleDevMode;
        toggleAction.Enable();
        Debug.LogWarning("DevTools enabled");
    }

    public void ToggleDevMode(InputAction.CallbackContext ctx)
    {
        if (devEnabled) DisableDevMode();
        else EnableDevMode();
        devEnabled = !devEnabled;
    }

    private void EnableDevMode()
    {
        foreach (var item in devCompnents)
        {
            (item as IDevAware)?.EnableDevMode();
        }

        this.GetComponent<Canvas>().enabled = true;
    }

    private void DisableDevMode()
    {
        foreach (var item in devCompnents)
        {
            (item as IDevAware)?.DisableDevMode();
        }
        this.GetComponent<Canvas>().enabled = false;
    }
}
