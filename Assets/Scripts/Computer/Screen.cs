using UnityEngine;

public class Screen : MonoBehaviour
{
    [SerializeField]
    private GameObject screen;

    public bool IsScreenActive { get; private set; }

    public void SetScreenActive(bool isActive)
    {
        IsScreenActive = isActive;
        screen.SetActive(isActive);
    }
}