using UnityEngine;

public class CaesarCipherTest : MonoBehaviour
{
    [SerializeField]
    private string stringToCipher = "ABCD";

    [SerializeField]
    private string cipherBase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    [SerializeField]
    private int shiftAmount = 5;

    private void Awake()
    {
        Debug.Log(CaesarCipher.Encode(stringToCipher, cipherBase, shiftAmount));
    }
}