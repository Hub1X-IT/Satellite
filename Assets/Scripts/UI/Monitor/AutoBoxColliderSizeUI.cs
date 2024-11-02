using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class AutoBoxColliderSizeUI : MonoBehaviour
{
    private void OnValidate()
    {
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        RectTransform rectTransform = GetComponent<RectTransform>();
        boxCollider.offset = (new Vector2(0.5f, 0.5f) - rectTransform.pivot) * rectTransform.sizeDelta;
        boxCollider.size = rectTransform.sizeDelta;
    }
}
