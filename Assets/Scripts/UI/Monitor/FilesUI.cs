using UnityEngine;
using UnityEngine.UI;

public class FilesUI : MonoBehaviour
{
    [SerializeField]
    private Button notepadButton;

    [SerializeField]
    private Canvas notepadCanvas;

    private bool notepadEnabled;

    private void Awake()
    {

        notepadButton.onClick.AddListener(NotepadEnable);
        notepadCanvas.enabled = false;
        //notepadButton.onClick.AddListener(() => SetNotepadEnabled(!notepadEnabled));
        //SetNotepadEnabled(false);
    }

    private void CloseAll()
    {
        notepadCanvas.enabled = false;
    }


     private void NotepadEnable()
     {
         Debug.Log("bazinga");
         if (notepadCanvas.enabled == false)
         {
             CloseAll();
             notepadCanvas.enabled = true;
         }
         else
         {
             notepadCanvas.enabled = false;
         }
     }


    /*private void SetNotepadEnabled(bool enabled)
    {
        CloseAll();
        notepadCanvas.enabled = enabled;
        notepadEnabled = enabled;
    }*/
    

}
