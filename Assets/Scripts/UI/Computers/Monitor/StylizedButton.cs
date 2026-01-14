using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StylizedButton : MonoBehaviour
{
    [SerializeField]
    private Image bkgImage;

    [SerializeField]
    private Sprite hoverSprite;

    [SerializeField]
    private Sprite selectSprite;

    private bool hovering = false;
    private bool selected = false;

    void ReloadState()
    {
        bkgImage.gameObject.SetActive(hovering || selected);
        bkgImage.sprite = (selected)? selectSprite : hoverSprite;
    }

    public void OnSelect()
    {
        selected = true;
        ReloadState();
    }

    public void OnDeselect()
    {
        selected = false;
        ReloadState();
    }

    public void OnHover()
    {
        hovering = true;
        ReloadState();
    }

    public void OnUnhover()
    {
        hovering = false;
        ReloadState();
    }
}
