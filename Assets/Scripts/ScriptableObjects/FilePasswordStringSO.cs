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
        string encodedPassword = PasswordEncryption.EncryptPassword(PasswordContent, numberOfEncryptions).Password;
        EncodedCompressedPasswordContent = TextCompressor.GetCompressedText(encodedPassword);
    }
}