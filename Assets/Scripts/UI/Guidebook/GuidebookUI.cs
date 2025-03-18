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
    private GameObject[] pages;

    [SerializeField]
    private Button[] pageListButtons;

    private void Awake()
    {
        foreach (var button in pageListButtons)
        {
            button.onClick.AddListener(() =>
            {
            for (int i = 0, y = 0; i < pages.Length && y < pageListButtons.Length; i++, y++)
                {
                    if (y == i+1)
                    {
                        pages[0].SetActive(false);
                        pages[1].SetActive(false);
                        pages[i + 1].gameObject.SetActive(true);
                        pages[i + 2].gameObject.SetActive(true);
                        break;
                    }
                }
            });
        }
        nextPageButton.onClick.AddListener(() =>
        {
            for (int i = 0; i < pages.Length; i++)
            {
                if (pages[i] == isActiveAndEnabled && pages[i + 1] == isActiveAndEnabled && i < pages.Length + 1)
                {
                    pages[i].SetActive(false);
                    pages[i + 1].SetActive(false);
                    pages[i + 2].SetActive(true);
                    pages[i + 3].SetActive(true);
                    break;
                }
            }
        });
        previousPageButton.onClick.AddListener(() =>
        {
            for (int i = 0; i < pages.Length; i++)
            {
                if (pages[i] == isActiveAndEnabled && pages[i + 1] == isActiveAndEnabled && i >= 2)
                {
                    pages[i].SetActive(false);
                    pages[i + 1].SetActive(false);
                    pages[i - 1].SetActive(true);
                    pages[i - 2].SetActive(true);
                    break;
                }
            }
        });
    }
}
