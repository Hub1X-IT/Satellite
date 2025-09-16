public class EncryptedPassword
{
    public string Password { get; set; }

    public string OriginalPassword { get; set; }

    public PasswordEncryption.CipherType[] UsedEncryptions { get; set; }

    public PasswordEncryption.EncryptionStep[] EncryptionSteps { get; set; }
}