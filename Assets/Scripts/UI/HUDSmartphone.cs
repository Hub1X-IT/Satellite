using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDSmartphone : MonoBehaviour
{
    public Image smartphoneImage;
    public GameObject Crosshair;
    public GameObject email;
    public Animator animator;

    bool smartphoneOn = false;

    private void Start()
    {
        smartphoneImage.rectTransform.localPosition = new Vector2(640, -800);
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            SmartphoneOnOff();
        }
    }


    void SmartphoneOnOff()
    {
        
        if (smartphoneOn == false)
        {
            animator.SetBool("TurnPhone", true);
            smartphoneOn = true;
            Crosshair.SetActive(false);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
        }
        else
        {
            animator.SetBool("TurnPhone", false);
            smartphoneOn = false;
            Crosshair.SetActive(true);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
        }
    }
}
