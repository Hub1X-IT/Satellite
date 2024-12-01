using System;
using UnityEngine;
using UnityEngine.UI;

public class ScreenUIButton : MonoBehaviour, IScreenUIInteractable
{
    private Image image;

    private Button button;

    [SerializeField]
    private Color normalColor = new(1f, 1f, 1f);

    [SerializeField]
    private Color highlightedColor = new(0.8f, 0.8f, 0.8f);

    [SerializeField]
    private Color pressedColor = new(0.6f, 0.6f, 0.6f);

    private bool isHighlighted;

    private bool isPressed;

    private float pressedTime;

    private const float PRESSED_TIME = 0.1f;

    /*
    public event Action OnLeftClick;

    public event Action OnRightClick;
    */

    public Transform SelfTransform { get; private set; }

    private void OnValidate()
    {
        GetComponent<Image>().color = normalColor;
    }

    private void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();

        isHighlighted = false;

        SelfTransform = transform;
    }

    /*
    private void OnDestroy()
    {
        OnLeftClick = null;
        OnRightClick = null;
    }
    */

    private void Update()
    {
        if (isPressed)
        {
            if (pressedTime <= 0f)
            {
                isPressed = false;
                SetHighlighted(isHighlighted);
            }
            else
            {
                pressedTime -= Time.deltaTime;
            }
        }
    }

    public void SetHighlighted(bool highlighted)
    {
        isHighlighted = highlighted;
        if (!isPressed)
        {
            image.color = isHighlighted ? highlightedColor : normalColor;
        }
    }

    public void LeftClick()
    {
        /*
        OnLeftClick?.Invoke();
        */
        button.onClick.Invoke();
        SetPressed();
    }
    
    public void RightClick()
    {
        Debug.Log($"{name}: right click");
        /*
        OnRightClick?.Invoke();
        */
    }

    private void SetPressed()
    {
        image.color = pressedColor;
        isPressed = true;
        pressedTime = PRESSED_TIME;
    }
}
