using UnityEngine;

public class CiphersTest : MonoBehaviour
{
    [SerializeField]
    private string stringToEncrypt = "ABCD";

    private void Awake()
    {
        Debug.Log("Caesar cipher: " + CaesarCipher.DefaultEncode(stringToEncrypt));
        Debug.Log("Atbash cipher: " + AtbashCipher.DefaultEncode(stringToEncrypt));
    }
}