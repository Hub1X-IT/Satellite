using UnityEngine;
using UnityEngine.UI;

public class MonitorUIStartMenu : MonoBehaviour
{
    [SerializeField]
    private Button optionsButton;

    private void Awake()
    {
        optionsButton.onClick.AddListener(() => { });
    }

    public void SetStartMenuActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
