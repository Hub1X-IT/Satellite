public class EncodedPassword
{
    public string Password { get; private set; }

    public int[] AsciiCodes { get; private set; }

    public EncryptedCharacter[] EncryptedCharacters { get; private set; }


    public EncodedPassword(string password)
    {
        Password = password;
        AsciiCodes = EncodeToAscii();
        EncryptedCharacters = EncryptCharacters();
    }


    private EncryptedCharacter[] EncryptCharacters()
    {
        EncryptedCharacter[] encryptedCharacters = new EncryptedCharacter[AsciiCodes.Length];
        for (int i = 0; i < AsciiCodes.Length; i++)
        {
            encryptedCharacters[i] = CalculationsGeneration.GetEncryptedCharacter(AsciiCodes[i]);
        }
        return encryptedCharacters;
    }

    private int[] EncodeToAscii()
    {
        int[] asciiCodes = new int[Password.Length];
        for (int i = 0; i < Password.Length; i++)
        {
            asciiCodes[i] = Password[i];
        }
        return asciiCodes;
    }
}
