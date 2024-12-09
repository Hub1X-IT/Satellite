using TMPro;
using UnityEngine;

public class PasswordEncodingTestObsolete : MonoBehaviour
{
    [SerializeField]
    private string password;

    [SerializeField]
    private Canvas testCanvas;

    [SerializeField]
    private TMP_Text outputField0;
    [SerializeField]
    private TMP_Text outputField1;
    [SerializeField]
    private TMP_Text outputField2;
    [SerializeField]
    private TMP_Text outputField3;

    [SerializeField]
    private TMP_InputField inputField0;
    [SerializeField]
    private TMP_InputField inputField1;
    [SerializeField]
    private TMP_InputField inputField2;
    [SerializeField]
    private TMP_InputField inputField3;

    private bool isCharacter0Correct = false;
    private bool isCharacter1Correct = false;
    private bool isCharacter2Correct = false;
    private bool isCharacter3Correct = false;

    private void Awake()
    {
        InitializeEncoding();

        inputField0.onEndEdit.AddListener((str) =>
        {
            isCharacter0Correct = str.Length == 1 && str[0] == password[0];
            CheckForCorrectPassword();
        });
        inputField1.onEndEdit.AddListener((str) =>
        {
            isCharacter1Correct = str.Length == 1 && str[0] == password[1];
            CheckForCorrectPassword();
        });
        inputField2.onEndEdit.AddListener((str) =>
        {
            isCharacter2Correct = str.Length == 1 && str[0] == password[2];
            CheckForCorrectPassword();
        });
        inputField3.onEndEdit.AddListener((str) =>
        {
            isCharacter3Correct = str.Length == 1 && str[0] == password[3];
            CheckForCorrectPassword();
        });
    }


    private void Update()
    {
        if (GameManager.HiddenCursorLockMode == CursorLockMode.Locked && Input.GetKeyDown(KeyCode.T))
        {
            TestEncoding();
        }
    }

    private void InitializeEncoding()
    {
        EncodedPasswordObsolete encodedPassword = new (this.password);
        string password = encodedPassword.Password;
        int[] asciiCodes = encodedPassword.AsciiCodes;
        EncryptedCharacterObsolete[] encryptedCharacters = encodedPassword.EncryptedCharacters;

        string asciiCodesDebugString = "";
        foreach (int asciiCode in asciiCodes) { asciiCodesDebugString += asciiCode.ToString() + "\n"; };
        string encryptedCharactersDebugString = "";
        foreach (EncryptedCharacterObsolete encryptedCharacter in encryptedCharacters)
        {
            encryptedCharactersDebugString += encryptedCharacter.EncryptedCharacterString + "\n";
        }

        /*
        Debug.Log(password);
        Debug.Log(asciiCodesDebugString);
        Debug.Log(encryptedCharactersDebugString);
        */

        outputField0.text = encryptedCharacters[0].EncryptedCharacterString;
        outputField1.text = encryptedCharacters[1].EncryptedCharacterString;
        outputField2.text = encryptedCharacters[2].EncryptedCharacterString;
        outputField3.text = encryptedCharacters[3].EncryptedCharacterString;
    }


    private void TestEncoding()
    {
        SetTestViewEnabled(true);
    }

    private void SetTestViewEnabled(bool enabled)
    {
        GameManager.SetCursorShown(enabled);
        GameManager.SetGamePaused(enabled);
        testCanvas.gameObject.SetActive(enabled);
    }

    private void CheckForCorrectPassword()
    {
        if (isCharacter0Correct && isCharacter1Correct && isCharacter2Correct && isCharacter3Correct)
        {
            Debug.Log("Password guessed!");
            SetTestViewEnabled(false);
        }
    }
}
