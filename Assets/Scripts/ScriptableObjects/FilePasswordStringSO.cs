using UnityEngine;

[CreateAssetMenu(menuName = "Monitor file system/FilePasswordStringSO")]
public class FilePasswordStringSO : FileStringSO
{
    [SerializeField]
    private DataContainerSO lockedDataContainerSO;

    [SerializeField]
    private PossiblePasswordsSO possiblePasswordsSO;

    [SerializeField]
    private int numberOfEncryptions = 3;

    public string PasswordContent { get; private set; }

    public string EncodedCompressedPasswordContent { get; private set; }

    public override void InitializeDataContainerSO()
    {
        base.InitializeDataContainerSO();
        PasswordContent = possiblePasswordsSO.GetRandomPassword();
        lockedDataContainerSO.DataContainerPassword = PasswordContent;
        EncryptedPassword encryptedPassword = PasswordEncryption.EncryptPassword(PasswordContent, numberOfEncryptions);
        EncryptedPasswordsManager.AddEncryptedPassword(encryptedPassword);
        string encodedPasswordString = encryptedPassword.Password;
        EncodedCompressedPasswordContent = TextCompressor.GetCompressedText(encodedPasswordString);

        // Probably temporary
        TempPasswordChecker.CorrectPassword = PasswordContent;
    }
}