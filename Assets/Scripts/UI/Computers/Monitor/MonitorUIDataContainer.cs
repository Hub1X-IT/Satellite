using TMPro;
using UnityEngine;

public abstract class MonitorUIDataContainer : MonoBehaviour
{
    [SerializeField]
    private TMP_Text nameTextField;

    public RectTransform SelfRectTransform { get; private set; }

    protected virtual void Awake()
    {
        SelfRectTransform = GetComponent<RectTransform>();
    }

    public void SetUIName(string name)
    {
        nameTextField.text = name;
    }
}