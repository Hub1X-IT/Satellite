public class EncodedPassword
{
    public string Password { get; private set; }

    public int[] AsciiCodes { get; private set; }

    public EncryptedCharacter[] EncryptedCharacters { get; private set; }

    public EncodedPassword(string password)
    {
        Password = password;
        AsciiCodes = EncodeToAscii(Password);
        EncryptedCharacters = EncryptCharacters(AsciiCodes);
    }

    private EncryptedCharacter[] EncryptCharacters(int[] asciiCodes)
    {
        EncryptedCharacter[] encryptedCharacters = new EncryptedCharacter[asciiCodes.Length];
        for (int i = 0; i < asciiCodes.Length; i++)
        {
            encryptedCharacters[i] = CalculationsGeneration.GetEncryptedCharacterForNumber(asciiCodes[i]);
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
