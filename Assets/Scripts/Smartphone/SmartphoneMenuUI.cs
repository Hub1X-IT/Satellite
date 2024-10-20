using UnityEngine;
using UnityEngine.UI;

public class SmartphoneMenuUI : MonoBehaviour {


    [SerializeField] private EmailUI emailUI;

    [SerializeField] private Button emailButton;


    private void Awake() {
        emailButton.onClick.AddListener(() => {
            emailUI.Show(Show);
            Hide();
            
        });
    }


    private void Show() {
        gameObject.SetActive(true);
    }


    private void Hide() {
        gameObject.SetActive(false);
    }
}