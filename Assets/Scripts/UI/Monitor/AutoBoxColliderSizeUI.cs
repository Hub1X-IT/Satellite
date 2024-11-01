using UnityEngine;

public class AutoBoxColliderSizeUI : MonoBehaviour
{
    private void OnValidate()
    {
        GetComponent<BoxCollider2D>().size = GetComponent<RectTransform>().sizeDelta;
    }
}
