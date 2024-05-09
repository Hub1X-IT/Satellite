using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Smartphone : MonoBehaviour
{

    public Image smartphone;
    public GameObject Crosshair;
    public GameObject email;
    public Animator animator;

    bool smartphoneOn = false;

    private void Start()
    {
        smartphone.rectTransform.localPosition = new Vector2(640, -800);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            SmartphoneOn();
        }
    }

    void SmartphoneOn()
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
