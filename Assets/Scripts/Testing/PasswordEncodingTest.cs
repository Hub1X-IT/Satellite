using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordEncodingTest : MonoBehaviour {

    [SerializeField] private string password;


    private void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            TestEncoding();
        }
    }


    public void TestEncoding() {
        EncodedPassword encodedPassword = new EncodedPassword(this.password);
        string password = encodedPassword.GetPassword();
        int[] asciiCodes = encodedPassword.GetAsciiCodes();
        EncryptedCharacter[] encryptedCharacters = encodedPassword.GetEncryptedCharacters();

        string asciiCodesDebugString = "";
        foreach (int asciiCode in asciiCodes) { asciiCodesDebugString += asciiCode.ToString() + "\n"; };
        string encryptedCharactersDebugString = "";
        foreach (EncryptedCharacter encryptedCharacter in encryptedCharacters) {
            encryptedCharactersDebugString += encryptedCharacter.GetEncryptedCharacterString() + "\n";
        }

        Debug.Log(password);
        Debug.Log(asciiCodesDebugString);
        Debug.Log(encryptedCharactersDebugString);
    }
}
