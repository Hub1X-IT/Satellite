using UnityEngine;

public class Screen : MonoBehaviour
{
    [SerializeField]
    private GameObject screen;


    private bool isEnabled;


    public bool IsEnabled
    {
        get => isEnabled;
        set
        {
            isEnabled = value;
            screen.SetActive(value);
        }
    }
}