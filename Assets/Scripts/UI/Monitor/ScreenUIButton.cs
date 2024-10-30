using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScreenUIButton : MonoBehaviour, IScreenUIInteractable
{
    private Button button;

    [SerializeField]
    private bool automaticallySetColliderSize;

    public Transform SelfTransform { get; private set; }

    private void Awake()
    {
        button = GetComponent<Button>();

        if (automaticallySetColliderSize)
        {
            GetComponent<BoxCollider2D>().size = GetComponent<RectTransform>().sizeDelta;
        }

        SelfTransform = transform;
    }

    public void LeftClick()
    {
        button.onClick.Invoke();
    }

    public void RightClick()
    {
        Debug.Log($"{name}: RightClick");
    }

    public void Select()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
        Debug.Log("Select");
    }

    public void Deselect()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }
}
