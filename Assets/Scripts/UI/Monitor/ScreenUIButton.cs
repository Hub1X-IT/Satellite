using System;
using UnityEngine;
using UnityEngine.UI;

public class ScreenUIButton : MonoBehaviour, IScreenUIInteractable
{
    private Image image;

    private Button button;

    [SerializeField]
    private Color normalColor = new(255, 255, 255, 255);

    [SerializeField]
    private Color highlightedColor = new(200, 200, 200, 255);

    [SerializeField]
    private Color pressedColor = new(150, 150, 150, 255);

    private bool isHighlighted;

    private bool isPressed;

    private float pressedTime;

    private const float PRESSED_TIME = 0.2f;

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
