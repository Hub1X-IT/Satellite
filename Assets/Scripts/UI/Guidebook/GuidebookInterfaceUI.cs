using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GuidebookInterfaceUI : MonoBehaviour
{
    [Serializable]
    private class PageAndButtons
    {
        public GameObject Page;
        public Button[] Buttons;
        public Button[] Bookmarks;
    }

    [Serializable]
    private class Bookmark
    {
        public Button LeftMark;
        public Button RightMark;
        public int PageNum;
    }

    // Left - even number (starting from 0)
    // Right - odd number (starting from 1)

    [SerializeField]
    private PageAndButtons[] pagesAndButtons;
    [SerializeField]
    private Bookmark[] bookmarks;

    [SerializeField]
    private Button nextPageButton;
    [SerializeField]
    private Button previousPageButton;

    [SerializeField]
    private Sprite bookmarkSpriteActive;
    [SerializeField]
    private Sprite bookmarkSpriteInactive;

    [SerializeField]
    private int currentPageNumber;

    private Dictionary<Button, int> buttonToPageNumber = new();

    private void Awake()
    {
        for (int i = 0; i < pagesAndButtons.Length; i++)
        {
            foreach (var button in pagesAndButtons[i].Buttons)
            {
                buttonToPageNumber.Add(button, i);
            }
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

        foreach (var bookmark in bookmarks)
        {
            void callback()
            {
                ChangeToPage(bookmark.PageNum);
            }

            bookmark.LeftMark.onClick.AddListener(callback);
            bookmark.RightMark.onClick.AddListener(callback);
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
        UpdateBookmarkVisuals();
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
        UpdateBookmarkVisuals();
    }

    private void SetPageActive(int pageNumber, bool active)
    {
        pagesAndButtons[pageNumber].Page.SetActive(active);
        if (pageNumber % 2 == 0 && pageNumber + 1 < pagesAndButtons.Length)
        {
            pagesAndButtons[pageNumber + 1].Page.SetActive(active);
        }
        else if (pageNumber % 2 != 0 && pageNumber - 1 >= 0)
        {
            pagesAndButtons[pageNumber - 1].Page.SetActive(active);
        }
    }

    private void UpdateBookmarkVisuals()
    {
        int mid = currentPageNumber / 2;
        
        foreach (var bookmark in bookmarks)
        {
            int two_page = bookmark.PageNum / 2;
            if (two_page < mid || (mid == two_page && bookmark.PageNum % 2 == 0)) {
                bookmark.LeftMark.gameObject.SetActive(true);
                bookmark.RightMark.gameObject.SetActive(false);
            } else {
                bookmark.LeftMark.gameObject.SetActive(false);
                bookmark.RightMark.gameObject.SetActive(true);
            }

            bookmark.LeftMark.image.sprite = bookmark.RightMark.image.sprite = (mid == two_page) ? bookmarkSpriteActive : bookmarkSpriteInactive;
        }
    }
}
