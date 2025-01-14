using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuidebookUI : MonoBehaviour
{
    [SerializeField]
    private Button nextPageButton;
    [SerializeField]
    private Button previousPageButton;
    [SerializeField]
    private TMP_Text textLeftPage;
    [SerializeField]
    private TMP_Text textRightPage;

    private int pageNumber;

    [SerializeField]
    private TextAsset titlePage;
    [SerializeField]
    private TextAsset basicsPage;
    [SerializeField]
    private TextAsset decimalPage;
    [SerializeField]
    private TextAsset binaryPage;
    [SerializeField]
    private TextAsset hexadecimalPage;
    [SerializeField]
    private TextAsset octalPage;
    [SerializeField]
    private TextAsset atbashPage;
    [SerializeField]
    private TextAsset caesarPage;

    private void Awake()
    {
        pageNumber = 1;
        ChangePageText();
        previousPageButton.onClick.AddListener(() =>
        {
            if (pageNumber > 1)
            {
                pageNumber--;
            }
            ChangePageText();
        });
        nextPageButton.onClick.AddListener(() =>
        {
            if (pageNumber < 4)
            {
                pageNumber++;
            }
            ChangePageText();
        });
    }

    private void ChangePageText()
    {
        switch (pageNumber)
        {
            case 1:
                textLeftPage.text = titlePage.text;
                textLeftPage.horizontalAlignment = HorizontalAlignmentOptions.Center;
                textLeftPage.fontSize = 40f;
                textRightPage.text = basicsPage.text;
                break;
            case 2:
                textLeftPage.text = decimalPage.text;
                textLeftPage.horizontalAlignment = HorizontalAlignmentOptions.Left;
                textLeftPage.fontSize = 27f;
                textRightPage.text = binaryPage.text;
                break;
            case 3:
                textLeftPage.text = hexadecimalPage.text;
                textRightPage.text = octalPage.text;
                break;
            case 4:
                textLeftPage.text = atbashPage.text;
                textRightPage.text = null;
                break;
        }
    }
}
