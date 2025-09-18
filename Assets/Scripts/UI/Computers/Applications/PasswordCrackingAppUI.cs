using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PasswordCrackingAppUI : MonoBehaviour
{
    private MonitorAppUI monitorAppUI;

    [SerializeField]
    private ConvertedPasswordUI convertedPasswordPrefab;
    [SerializeField]
    private Transform convertedPasswordsHolder;
    [SerializeField]
    private ConvertedPasswordUI decompressedPasswordUI;
    [SerializeField]
    private TMP_InputField inputField;

    [SerializeField]
    private Button resetPasswordCrackingButton;
    [SerializeField]
    private Button pasteAndDecompressButton;

    [SerializeField]
    private Button undoAllStepsButton;
    [SerializeField]
    private Button undoLastStepButton;

    [SerializeField]
    private Button binButton;
    [SerializeField]
    private Button octButton;
    [SerializeField]
    private Button decButton;
    [SerializeField]
    private Button hexButton;
    [SerializeField]
    private Button atbashButton;
    [SerializeField]
    private Button caesarButton;
    [SerializeField]
    private TMP_InputField caesarParameterInputField;

    private string decompressedPassword;
    private Stack<ConvertedPasswordUI> previousConvertedPasswordUIStack;
    private EncryptedPassword currentEncryptedPassword;
    private PasswordEncryption.EncryptionStep[] passwordEncryptionSteps;
    private int encryptionStepIndex;

    [SerializeField]
    private TMP_Text detectionChanceTextField;
    private const string DetectionChanceText = "Detection Chance: ";

    // Probably temporary
    public event Action<string> NewPasswordConverted;
    private bool wasDetected;

    private void OnDestroy()
    {
        NewPasswordConverted = null;
    }

    public void InitializePasswordCrackingApp(string appName)
    {
        monitorAppUI = GetComponent<MonitorAppUI>();
        monitorAppUI.SetAppName(appName);
        monitorAppUI.DestroyOnClose = false;

        InitializePasswordCracking();
        SetDetectionChanceText();

        TempPasswordChecker.SetPasswordCrackingAppReference(this);

        DetectionManager.DetectionOccured += () =>
        {
            // DisableApp();
            wasDetected = true;
            if (monitorAppUI != null)
            {
                monitorAppUI.DestroyOnClose = true;
                monitorAppUI.CloseApp();
            }
        };
        DetectionManager.DetectionRemoved += () =>
        {
            // EnableApp();
            SetDetectionChanceText();
        };

        wasDetected = false;
    }

    private void InitializePasswordCracking()
    {
        previousConvertedPasswordUIStack = new();

        decompressedPassword = string.Empty;
        decompressedPasswordUI.gameObject.SetActive(false);

        if (!DetectionManager.WasDetected)
        {
            EnableApp();
        }
        else
        {
            DisableApp();
        }
    }

    private void EnableApp()
    {
        // inputField.onEndEdit.AddListener(ChangeOriginalPassword);
        resetPasswordCrackingButton.onClick.AddListener(ResetPasswordCracking);
        pasteAndDecompressButton.onClick.AddListener(PasteAndDecompress);
        inputField.onEndEdit.AddListener(InputField_OnEndEdit);

        undoAllStepsButton.onClick.AddListener(UndoAllSteps);
        undoLastStepButton.onClick.AddListener(UndoLastStep);

        binButton.onClick.AddListener(BinDecode);
        octButton.onClick.AddListener(OctDecode);
        decButton.onClick.AddListener(DecDecode);
        hexButton.onClick.AddListener(HexDecode);
        atbashButton.onClick.AddListener(AtbashDecode);
        caesarButton.onClick.AddListener(CaesarDecode);
    }

    private void DisableApp()
    {
        // inputField.onEndEdit.RemoveListener(ChangeOriginalPassword);
        resetPasswordCrackingButton.onClick.RemoveListener(ResetPasswordCracking);
        pasteAndDecompressButton.onClick.RemoveListener(PasteAndDecompress);
        inputField.onEndEdit.RemoveListener(InputField_OnEndEdit);

        undoAllStepsButton.onClick.RemoveListener(UndoAllSteps);
        undoLastStepButton.onClick.RemoveListener(UndoLastStep);

        binButton.onClick.RemoveListener(BinDecode);
        octButton.onClick.RemoveListener(OctDecode);
        decButton.onClick.RemoveListener(DecDecode);
        hexButton.onClick.RemoveListener(HexDecode);
        atbashButton.onClick.RemoveListener(AtbashDecode);
        caesarButton.onClick.RemoveListener(CaesarDecode);
    }

    private void InputField_OnEndEdit(string inputText)
    {
        DecompressPassword(inputText);
    }
    private void DecompressPassword(string compressedPassword)
    {
        if (TextCompressor.TryGetDecompressedText(compressedPassword, out decompressedPassword))
        {
            RemoveAllPasswordTextFields();

            List<EncryptedPassword> encryptedPasswordList = EncryptedPasswordsManager.EncryptedPasswords;
            foreach (var encryptedPassword in encryptedPasswordList)
            {
                if (encryptedPassword.Password == decompressedPassword)
                {
                    currentEncryptedPassword = encryptedPassword;
                    break;
                }
            }

            if (currentEncryptedPassword == null)
            {
                Debug.LogWarning("No encrypted password object found corresponding to the current encrypted password!");
                return;
            }

            Debug.Log("Current encrypted password: " + currentEncryptedPassword.Password + "\nOriginal password: " + currentEncryptedPassword.OriginalPassword);

            ResetPasswordEncryptionSteps();

            decompressedPasswordUI.InitializeConvertedPasswordUI(decompressedPassword);
            decompressedPasswordUI.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("Decompression failed.");
            decompressedPasswordUI.gameObject.SetActive(false);
        }
    }
    private void PasteAndDecompress()
    {
        inputField.text = VirtualClipboard.GetClipboardText();
        DecompressPassword(VirtualClipboard.GetClipboardText());
    }

    private void ResetPasswordEncryptionSteps()
    {
        passwordEncryptionSteps = currentEncryptedPassword.EncryptionSteps;
        encryptionStepIndex = passwordEncryptionSteps.Length - 1;
    }

    private void BinDecode() => TryAddDecodedPassword(PasswordEncryption.CipherType.ASCII_Base2);
    private void OctDecode() => TryAddDecodedPassword(PasswordEncryption.CipherType.ASCII_Base8);
    private void DecDecode() => TryAddDecodedPassword(PasswordEncryption.CipherType.ASCII_Base10);
    private void HexDecode() => TryAddDecodedPassword(PasswordEncryption.CipherType.ASCII_Base16);
    private void AtbashDecode() => TryAddDecodedPassword(PasswordEncryption.CipherType.AtbashCipher);
    private void CaesarDecode() => TryAddDecodedPassword(PasswordEncryption.CipherType.CaesarCipher);

    private void TryAddDecodedPassword(PasswordEncryption.CipherType usedCipher)
    {
        if (passwordEncryptionSteps == null)
        {
            Debug.Log("No password loaded.");
            return;
        }
        if (encryptionStepIndex < 0 || encryptionStepIndex >= passwordEncryptionSteps.Length)
        {
            Debug.Log("Invalid encryption step index.");
            return;
        }

        PasswordEncryption.EncryptionStep currentEncryptionStep = passwordEncryptionSteps[encryptionStepIndex];

        if (usedCipher == currentEncryptionStep.usedCipherType)
        {
            Debug.Log("Decoding step correct");
            encryptionStepIndex--;
            string convertedPassword = currentEncryptionStep.PreviousPasswordState;
            CreateNewPasswordTextField(convertedPassword);

            if (!wasDetected)
            {
                NewPasswordConverted?.Invoke(convertedPassword);

                // May be unnecessary now when checking decoding steps is implemented
            }

            if (encryptionStepIndex == 0)
            {
                // Player guessed the correct password.
            }
        }
        else
        {
            Debug.Log("Decoding step incorrect");
            DetectionManager.CheckDetection();
            SetDetectionChanceText();
        }
    }

    private void SetDetectionChanceText()
    {
        detectionChanceTextField.text = DetectionChanceText + DetectionManager.CurrentDetectionChance + "%";
    }

    private void CreateNewPasswordTextField(string newPassword)
    {
        ConvertedPasswordUI convertedPasswordUI = Instantiate(convertedPasswordPrefab.gameObject,
            convertedPasswordsHolder).GetComponent<ConvertedPasswordUI>();

        convertedPasswordUI.InitializeConvertedPasswordUI(newPassword);
        previousConvertedPasswordUIStack.Push(convertedPasswordUI);
    }

    private void RemoveAllPasswordTextFields()
    {
        while (previousConvertedPasswordUIStack.TryPop(out ConvertedPasswordUI convertedPasswordUI))
        {
            convertedPasswordUI.DestroySelf();
        }
    }

    private void ResetPasswordCracking()
    {
        inputField.text = string.Empty;
        RemoveAllPasswordTextFields();
        decompressedPasswordUI.gameObject.SetActive(false);
    }

    private void UndoAllSteps()
    {
        RemoveAllPasswordTextFields();
        ResetPasswordEncryptionSteps();
    }

    private void UndoLastStep()
    {
        if (previousConvertedPasswordUIStack.TryPop(out ConvertedPasswordUI currentConvertedPasswordUI))
        {
            currentConvertedPasswordUI.DestroySelf();
            Debug.Log("Destroyed: " + currentConvertedPasswordUI);
            encryptionStepIndex++;
        }
    }
}
