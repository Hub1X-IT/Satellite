using UnityEngine;
using UnityEngine.UI;

public class Fullscreen : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private Toggle FullscreenToggle;
    public void Fullscreener(bool toggleValue)
    {
            Screen.fullScreen = toggleValue;
       if (toggleValue )
            Debug.Log("El Full");
       else
            Debug.Log("El Empty");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
