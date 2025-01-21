using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuidebookUINew : MonoBehaviour
{
    [SerializeField]
    private Button nextPageButton;
    [SerializeField]
    private Button previousPageButton;
    [SerializeField]
    private TMP_Text leftPageTextField;
    [SerializeField]
    private TMP_Text rightPageTextField;

    [SerializeField]
    private TextAsset[] pageAssets;

    private int currentPageNumber;

    private int maxPageNumber;

    private void Awake()
    {
        currentPageNumber = 1;
        maxPageNumber = pageAssets.Length;

        previousPageButton.onClick.AddListener(() =>
        {
            SetPage(currentPageNumber - 2);
        });
        nextPageButton.onClick.AddListener(() =>
        {
            SetPage(currentPageNumber + 2);
        });
    }

    private void SetPage(int newPageNumber)
    {
        currentPageNumber = Mathf.Clamp(newPageNumber, 1, maxPageNumber);

        TextAsset leftPageTextAsset = null;
        TextAsset rightPageTextAsset = null;
        if (currentPageNumber % 2 == 0 && currentPageNumber > 0)
        {
            leftPageTextAsset = pageAssets[currentPageNumber - 2];
            rightPageTextAsset = pageAssets[currentPageNumber - 1];
        }
        else if (currentPageNumber % 2 == 1)
        {
            leftPageTextAsset = pageAssets[currentPageNumber - 1];
            if (currentPageNumber < maxPageNumber)
            {
                rightPageTextAsset = pageAssets[currentPageNumber];
            }
        }

        leftPageTextField.text = leftPageTextAsset != null ? leftPageTextAsset.text : string.Empty;
        rightPageTextField.text = rightPageTextAsset != null ? rightPageTextAsset.text : string.Empty;
    }
}