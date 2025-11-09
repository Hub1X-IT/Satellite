using System;
using UnityEngine;
using UnityEngine.UI;

public class DoubleClickButtonUI : MonoBehaviour
{
    [SerializeField]
    private Button targetButton;

    private float lastClickTime;
    private const float doubleClickThreshold = 0.3f;
    public event Action OnDoubleClick;

    private void Awake()
    {
        targetButton.onClick.AddListener(HandleClick);
    }

    private void HandleClick()
    {
        float timeSinceLastClick = Time.time - lastClickTime;

        if (timeSinceLastClick <= doubleClickThreshold)
        {
            OnDoubleClick?.Invoke();
        }

        lastClickTime = Time.time;
    }
}