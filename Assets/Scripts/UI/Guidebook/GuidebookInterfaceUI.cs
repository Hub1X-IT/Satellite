using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuidebookInterfaceUI : MonoBehaviour
{
    [Serializable]
    private class PageAndButtons
    {
        public GameObject Page;
        public Button[] Buttons;
        public Button Bookmarks;
    }

    // Left - even number (starting from 0)
    // Right - odd number (starting from 1)

    [SerializeField]
    private PageAndButtons[] pagesAndButtons;

    [SerializeField]
    private Button nextPageButton;
    [SerializeField]
    private Button previousPageButton;

    [SerializeField]
    private Sprite bookmarkSpriteActive;
    [SerializeField]
    private Sprite bookmarkSpriteInactive;

    private int currentPageNumber;

    private Dictionary<Button, int> buttonToPageNumber = new();
    private Dictionary<Button, int> bookmarkButtonToPageNumber = new();

    private void Awake()
    {
        for (int i = 0; i < pagesAndButtons.Length; i++)
        {
            foreach (var button in pagesAndButtons[i].Buttons)
            {
                buttonToPageNumber.Add(button, i);
            }
        }

        /*
        for (int i = 0; i < pageListButtons.Length; i++)
        {
            if (pageListButtons[i] != null)
            {
                buttonToPageNumber.Add(pageListButtons[i], i);
            }
        }
        */

        for (int i = 0; i < pagesAndButtons.Length; i++)
        {
            bookmarkButtonToPageNumber.Add(pagesAndButtons[i].Bookmarks, i);
        }
        
        currentPageNumber = 0;
    }

    private void Start()
    {
        GameInput.Instance.OnGuidebookChangePageLeftAction += () =>
        {
            ChangeToPage(currentPageNumber + 2);
        };

        GameInput.Instance.OnGuidebookChangePageRightAction += () =>
        {
            ChangeToPage(currentPageNumber - 2);
        };

        foreach (var button in buttonToPageNumber.Keys)
        {
            button.onClick.AddListener(() =>
            {
                ChangeToPage(buttonToPageNumber[button]);
            });
        }

        foreach (var bookmark in bookmarkButtonToPageNumber.Keys)
        {
            bookmark.onClick.AddListener(() =>
            {
                ChangeToPage(bookmarkButtonToPageNumber[bookmark]);
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

        DisableAllPages();
        SetPageActive(currentPageNumber, true);
    }

    private void DisableAllPages()
    {
        foreach (var pageAndButton in pagesAndButtons)
        {
            pageAndButton.Page.SetActive(false);
        }
    }

    public void ChangeToPage(int newPageNumber)
    {
        SetPageActive(currentPageNumber, false);
        currentPageNumber = Mathf.Clamp(newPageNumber, 0, pagesAndButtons.Length - 1);
        SetPageActive(currentPageNumber, true);
    }

    private void SetPageActive(int pageNumber, bool active)
    {
        pagesAndButtons[pageNumber].Page.SetActive(active);
        pagesAndButtons[pageNumber].Bookmarks.image.sprite = active ? bookmarkSpriteActive : bookmarkSpriteInactive;
        if (pageNumber % 2 == 0 && pageNumber + 1 < pagesAndButtons.Length)
        {
            pagesAndButtons[pageNumber + 1].Page.SetActive(active);
        }
        else if (pageNumber % 2 != 0 && pageNumber - 1 >= 0)
        {
            pagesAndButtons[pageNumber - 1].Page.SetActive(active);
        }
    }
}
