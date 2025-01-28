using UnityEngine;

public class CommandPromptScroll : MonoBehaviour
{
    [SerializeField]
    private RectTransform content;
    [SerializeField]
    private RectTransform viewport;

    private const float ScrollSpeed = 10f;

    private void Update()
    {
        float scrollValue = GameInput.MouseScroll;
        content.anchoredPosition += new Vector2(0, scrollValue * ScrollSpeed);
        // ClampPosition();
    }

    private void ClampPosition()
    {
        Debug.Log($"{viewport.anchoredPosition} {viewport.position}");
        float upperBound = viewport.position.y + viewport.sizeDelta.y / 2;
        float lowerBound = viewport.position.y - viewport.sizeDelta.y / 2;
        float contentUpperBound = content.position.y + content.sizeDelta.y / 2;
        float contentLowerBound = content.position.y - content.sizeDelta.y / 2;

        if (contentUpperBound < upperBound)
        {

        }
    }
}