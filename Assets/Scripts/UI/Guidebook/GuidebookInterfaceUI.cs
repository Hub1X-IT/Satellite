using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuidebookInterfaceUI : MonoBehaviour
{
    // Left - even number (starting from 0)
    // Right - odd number (starting from 1)

    [SerializeField]
    private GameObject[] pages;

    // Button index must match the corresponding page index
    [SerializeField]
    private Button[] pageListButtons;

    [SerializeField]
    private Button nextPageButton;
    [SerializeField]
    private Button previousPageButton;

    private int currentPageNumber;

    private Dictionary<Button, int> buttonToPageNumber = new();

    private void Awake()
    {
        GameInput.OnGuidebookChangePageLeftAction += () =>
        {
            ChangeToPage(currentPageNumber + 2);
        };

        GameInput.OnGuidebookChangePageRightAction += () =>
        {
            ChangeToPage(currentPageNumber - 2);
        };

        for (int i = 0; i < pageListButtons.Length; i++)
        {
            if (pageListButtons[i] != null)
            {
                buttonToPageNumber.Add(pageListButtons[i], i);
            }
        }

        foreach (var button in buttonToPageNumber.Keys)
        {
            button.onClick.AddListener(() =>
            {
                ChangeToPage(buttonToPageNumber[button]);
            });
        }

        nextPageButton.onClick.AddListener(() =>
        {
            ChangeToPage(currentPageNumber + 2);
        });
        previousPageButton.onClick.AddListener(() =>
        {
            ChangeToPage(currentPageNumber - 2);
        });

        currentPageNumber = 0;
        DisableAllPages();
        SetPageActive(currentPageNumber, true);
    }

    private void DisableAllPages()
    {
        foreach (var page in pages)
        {
            page.SetActive(false);
        }
    }

    public void ChangeToPage(int newPageNumber)
    {
        SetPageActive(currentPageNumber, false);
        currentPageNumber = Mathf.Clamp(newPageNumber, 0, pages.Length - 1);
        SetPageActive(currentPageNumber, true);
    }

    private void SetPageActive(int pageNumber, bool active)
    {
        pages[pageNumber].SetActive(active);
        if (pageNumber % 2 == 0 && pageNumber + 1 < pages.Length)
        {
            pages[pageNumber + 1].SetActive(active);
        }
        else if (pageNumber % 2 != 0 && pageNumber - 1 >= 0)
        {
            pages[pageNumber - 1].SetActive(active);
        }
    }
}
