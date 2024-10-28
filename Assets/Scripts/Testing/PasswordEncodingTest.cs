using UnityEngine;

public class PasswordEncodingTest : MonoBehaviour
{
    [SerializeField]
    private string password;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TestEncoding();
        }
    }


    public void TestEncoding()
    {
        EncodedPassword encodedPassword = new EncodedPassword(this.password);
        string password = encodedPassword.Password;
        int[] asciiCodes = encodedPassword.AsciiCodes;
        EncryptedCharacter[] encryptedCharacters = encodedPassword.EncryptedCharacters;

        string asciiCodesDebugString = "";
        foreach (int asciiCode in asciiCodes) { asciiCodesDebugString += asciiCode.ToString() + "\n"; };
        string encryptedCharactersDebugString = "";
        foreach (EncryptedCharacter encryptedCharacter in encryptedCharacters)
        {
            encryptedCharactersDebugString += encryptedCharacter.EncryptedCharacterString + "\n";
        }

        Debug.Log(password);
        Debug.Log(asciiCodesDebugString);
        Debug.Log(encryptedCharactersDebugString);
    }
}
