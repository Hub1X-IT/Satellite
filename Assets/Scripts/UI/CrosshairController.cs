using UnityEngine;

public class CrosshairController : MonoBehaviour {


    [SerializeField] GameObject crosshair;


    public static CrosshairController Instance { get; private set; }


    private void Awake() {
        Instance = this;
    }


    public void ShowCrosshair() {
        crosshair.SetActive(true);
    }


    public void HideCrosshair() {
        crosshair.SetActive(false);
    }
}
