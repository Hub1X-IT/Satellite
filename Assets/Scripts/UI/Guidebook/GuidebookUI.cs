using UnityEngine;

public class GuidebookUI : MonoBehaviour
{
    private RectTransform guidebookRectTransform;

    [SerializeField]
    private Vector2 defaulGuidebookPosition = new(1580f, 0f);

    private bool isGuidebookEnabled;

    private void Awake()
    {
        guidebookRectTransform = GetComponent<RectTransform>();

        guidebookRectTransform.localPosition = defaulGuidebookPosition;

        isGuidebookEnabled = false;
    }

    private void OnEnable()
    {
        GameInput.OnGuidebookToggleAction += EnableDisableGuidebook;
    }

    private void OnDisable()
    {
        GameInput.OnGuidebookToggleAction -= EnableDisableGuidebook;
    }

    private void EnableDisableGuidebook()
    {
        SetGuidebookEnabled(!isGuidebookEnabled);
    }

    private void SetGuidebookEnabled(bool enabled)
    {
        isGuidebookEnabled = enabled;

        GameManager.SetGamePaused(enabled);

        if (enabled)
        {
            guidebookRectTransform.localPosition = Vector2.zero;
        }
        else
        {
            guidebookRectTransform.localPosition = defaulGuidebookPosition;
        }
        Debug.Log("guidebook");
    }
}
