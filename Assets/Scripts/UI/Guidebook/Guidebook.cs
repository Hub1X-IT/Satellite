using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Guidebook : MonoBehaviour
{
    [SerializeField]
    private Button nextPageButton;

    [SerializeField]
    private Button prevPageButton;

    [SerializeField]
    private TMP_Text textLeftPage;

    [SerializeField]
    private TMP_Text textRightPage;

    private int pageNum = 1;

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
        pageNum = 1;
        ChangePageText();
        prevPageButton.onClick.AddListener(() =>
        {
            if(pageNum > 1)
            {
                pageNum--;
            }
            ChangePageText();
        });
        nextPageButton.onClick.AddListener(() =>
        {
            if(pageNum < 4)
            {
                pageNum++;
            }
            ChangePageText();
        });
    }

    private void ChangePageText()
    {
        switch(pageNum)
        {
            case 1:
                textLeftPage.text = titlePage.text;
                textLeftPage.horizontalAlignment = HorizontalAlignmentOptions.Center;
                textRightPage.text = basicsPage.text;
                break;
            case 2:
                textLeftPage.text = decimalPage.text;
                textLeftPage.horizontalAlignment = HorizontalAlignmentOptions.Left;
                textRightPage.text = binaryPage.text;
                break;
            case 3:
                textLeftPage.text = hexadecimalPage.text;
                textRightPage.text = octalPage.text;
                break;
            case 4:
                textLeftPage.text = atbashPage.text;
                textRightPage.text = caesarPage.text;
                break;
        }
    }
}
