using UnityEngine;

public class CommandPromptScroll : MonoBehaviour
{
    [SerializeField]
    private RectTransform content;

    private const float ScrollSpeed = 50f;

    private void Update()
    {
        float scrollValue = GameInput.MouseScroll;
        content.anchoredPosition -= new Vector2(0, scrollValue * ScrollSpeed);
    }
}