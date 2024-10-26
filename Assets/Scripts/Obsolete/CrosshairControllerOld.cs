using UnityEngine;

public class CrosshairControllerOld : MonoBehaviour {

    [SerializeField] private CanvasRenderer crosshair;

    public void ShowCrosshair(bool targetState) {
        crosshair.gameObject.SetActive(targetState);
    }
}