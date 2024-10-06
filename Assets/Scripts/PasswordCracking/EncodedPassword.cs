public class EncodedPassword {

    private string password;
    
    private int[] asciiCodes;
    private EncryptedCharacter[] encryptedCharacters;


    public EncodedPassword(string password) {
        this.password = password;
        asciiCodes = EncodeToAscii();
        encryptedCharacters = EncryptCharacters();
    }


    private EncryptedCharacter[] EncryptCharacters() {
        EncryptedCharacter[] encryptedCharacters = new EncryptedCharacter[asciiCodes.Length];
        for (int i = 0; i < asciiCodes.Length; i++) {
            encryptedCharacters[i] = CalculationsGeneration.GetEncryptedCharacter(asciiCodes[i]);
        }
        return encryptedCharacters;
    }

    private int[] EncodeToAscii() {
        int[] asciiCodes = new int[password.Length];
        for (int i = 0; i < password.Length; i++) { asciiCodes[i] = password[i]; }
        /*
        List<int> intList = new List<int>();
        foreach (int i in password) { intList.Add(i); }
        return intList.ToArray();
        */
        return asciiCodes;
    }


    public string GetPassword() { return password; }
    public int[] GetAsciiCodes() { return asciiCodes; }
    public EncryptedCharacter[] GetEncryptedCharacters() { return encryptedCharacters; }
}
