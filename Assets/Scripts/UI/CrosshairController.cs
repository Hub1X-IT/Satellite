using UnityEngine;

public class CrosshairController : MonoBehaviour {

    [SerializeField] private CanvasRenderer crosshair;

    public void ShowCrosshair(bool targetState) {
        crosshair.gameObject.SetActive(targetState);
    }
}