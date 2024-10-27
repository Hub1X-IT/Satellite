using UnityEngine;

public class CrosshairControllerObsolete : MonoBehaviour
{
    [SerializeField] private CanvasRenderer crosshair;

    public void ShowCrosshair(bool targetState)
    {
        crosshair.gameObject.SetActive(targetState);
    }
}