public class EncodedPasswordObsolete
{
    public string Password { get; private set; }

    public int[] AsciiCodes { get; private set; }

    public EncryptedCharacterObsolete[] EncryptedCharacters { get; private set; }

    public EncodedPasswordObsolete(string password)
    {
        Password = password;
        AsciiCodes = EncodeToAscii(Password);
        EncryptedCharacters = EncryptCharacters(AsciiCodes);
    }

    private EncryptedCharacterObsolete[] EncryptCharacters(int[] asciiCodes)
    {
        EncryptedCharacterObsolete[] encryptedCharacters = new EncryptedCharacterObsolete[asciiCodes.Length];
        for (int i = 0; i < asciiCodes.Length; i++)
        {
            encryptedCharacters[i] = CalculationsGenerationObsolete.GetEncryptedCharacterForNumber(asciiCodes[i]);
        }
        return encryptedCharacters;
    }

    private int[] EncodeToAscii(string str)
    {
        int[] asciiCodes = new int[str.Length];
        for (int i = 0; i < str.Length; i++)
        {
            asciiCodes[i] = str[i];
        }
        return asciiCodes;
    }
}
