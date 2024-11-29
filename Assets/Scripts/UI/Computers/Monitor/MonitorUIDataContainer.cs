using TMPro;
using UnityEngine;

public abstract class MonitorUIDataContainer : MonoBehaviour
{
    [SerializeField]
    protected TMP_Text nameTextField;

    public RectTransform SelfRectTransform { get; private set; }

    protected virtual void Awake()
    {
        SelfRectTransform = GetComponent<RectTransform>();
    }

    public void SetName(string newName)
    {
        gameObject.name = name = nameTextField.text = newName;
    }

    public void DestroySelf()
    {
        SetName(name + "_Destroyed");
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}