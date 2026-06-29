using System;
using TMPro;
using UnityEngine;

public class ObjectivesUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text chapterTextField;

    [SerializeField]
    private TMP_Text objectiveTextField;

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private RectTransform hitRect;

    [SerializeField, Min(0)]
    private int hitBorderWidth;

    private long hitBorderWidthPow;

    private void Awake()
    {
        hitBorderWidthPow = hitBorderWidth * hitBorderWidth;
    }

    private void Start()
    {
        ObjectivesManager.Instance.OnChapterChanged += SetChapter;
        ObjectivesManager.Instance.OnObjectiveChanged += SetObjective;
    }

    private void Update()
    {
        // var dx = Math.Max(hitRect.si);
        Vector3[] corners = new Vector3[4];
        hitRect.GetWorldCorners(corners);
        Vector2 mousePos = GameInput.Instance.MouseScreenPos;

        // Suared distance point-rectangle
        var dx = Math.Max(Math.Max(corners[0].x - mousePos.x, 0), mousePos.x - corners[2].x);
        var dy = Math.Max(Math.Max(corners[0].y - mousePos.y, 0), mousePos.y - corners[2].y);
        var distSquared = dx*dx + dy*dy;

        // Squared lerp
        if (distSquared >= hitBorderWidthPow) canvasGroup.alpha = 1;
        else if (distSquared <= 0) canvasGroup.alpha = 0;
        else canvasGroup.alpha = distSquared / hitBorderWidthPow;
    }

    public void SetChapter(string chapter)
    {
        chapterTextField.text = chapter;
    }

    private void SetObjective(string objective)
    {
        objectiveTextField.text = objective;
    }
}