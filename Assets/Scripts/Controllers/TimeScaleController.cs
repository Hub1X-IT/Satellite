using UnityEngine;

public class TimeScaleController : MonoBehaviour {


    public static TimeScaleController Instance { get; private set; }


    private void Awake() {
        Instance = this;
    }


    public void StopTime() {
        Time.timeScale = 0f;
    }


    public void StartTime() {
        Time.timeScale = 1f;
    }


    public void SetTimeScale(float timeScale) {
        Time.timeScale = timeScale;
    }
}
