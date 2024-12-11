using UnityEngine;

public class CiphersTest : MonoBehaviour
{
    [SerializeField]
    private string stringToEncrypt;

    [SerializeField]
    private int numberOfEncryptions;

    [SerializeField]
    private string stringToDecode;

    [SerializeField]
    private int decodeBase;

    [SerializeField]
    private Canvas testCanvas;

    private bool isTestCanvasActive;

    private void Awake()
    {
        // Debug.Log("Caesar cipher: " + CaesarCipher.DefaultEncode(stringToEncrypt));
        // Debug.Log("Atbash cipher: " + AtbashCipher.DefaultEncode(stringToEncrypt));

        Test();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Test();
        }
        if (Input.GetKeyDown(KeyCode.T) && GameManager.HiddenCursorLockMode == CursorLockMode.Locked)
        {
            // SetTestUIEnabled(!isTestCanvasActive);
        }
    }

    private void SetTestUIEnabled(bool enabled)
    {
        isTestCanvasActive = enabled;
        testCanvas.gameObject.SetActive(enabled);
        GameManager.SetCursorShown(enabled);
        GameManager.SetGamePaused(enabled);
    }

    private void Test()
    {
        EncryptedPassword encryptedPassword = PasswordEncryption.EncryptPassword(stringToEncrypt, numberOfEncryptions);
        string debugString = $"Encrypted password: {encryptedPassword.Password}\nOriginal password: {encryptedPassword.OriginalPassword}\nUsed encryptions: ";
        foreach (var encryption in encryptedPassword.UsedEncryptions)
        {
            debugString += encryption + ", ";
        }
        Debug.Log(debugString);

        Debug.Log($"Decoded string: {ASCIIEncryption.Decode(stringToDecode, decodeBase)}");
    }
}