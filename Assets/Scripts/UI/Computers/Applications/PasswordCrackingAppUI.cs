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
    private TMP_InputField inputField;
    [SerializeField]
    private Button pasteAndDecompressButton;

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

    private string currentPassword;
    private string originalPassword;
    private Stack<ConvertedPasswordUI> previousConvertedPasswordUIStack;

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

        InitializePasswordCracking();
        SetDetectionChanceText();

        TempPasswordChecker.SetPasswordCrackingAppReference(this);

        DetectionManager.DetectionOccured += () =>
        {
            // DisableApp();
            wasDetected = true;
            if (monitorAppUI != null)
            {
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

        originalPassword = currentPassword = "";

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
        pasteAndDecompressButton.onClick.AddListener(PasteAndDecompress);
        inputField.onEndEdit.AddListener(InputField_OnEndEdit);

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
        pasteAndDecompressButton.onClick.RemoveListener(PasteAndDecompress);
        inputField.onEndEdit.RemoveListener(InputField_OnEndEdit);

        binButton.onClick.RemoveListener(BinDecode);
        octButton.onClick.RemoveListener(OctDecode);
        decButton.onClick.RemoveListener(DecDecode);
        hexButton.onClick.RemoveListener(HexDecode);
        atbashButton.onClick.RemoveListener(AtbashDecode);
        caesarButton.onClick.RemoveListener(CaesarDecode);
    }

    private void ChangeOriginalPassword(string newPassword)
    {
        RemoveAllPasswordTextFields();
        originalPassword = currentPassword = newPassword;
    }
    private void InputField_OnEndEdit(string inputText)
    {
        DecompressPassword(inputText);
    }
    private void DecompressPassword(string compressedPassword)
    {
        if (TextCompressor.TryGetDecompressedText(compressedPassword, out string decompressedPassword))
        {
            RemoveAllPasswordTextFields();
            currentPassword = decompressedPassword;
            CreateNewPasswordTextField(decompressedPassword);
        }
        else
        {
            Debug.Log("Decompression failed.");
        }
    }
    private void PasteAndDecompress()
    {
        inputField.text = VirtualClipboard.GetClipboardText();
        DecompressPassword(VirtualClipboard.GetClipboardText());
    }

    private void BinDecode()
    {
        int decodeBase = 2;
        currentPassword = ASCIIEncryption.Decode(currentPassword, decodeBase);
        AddDecodedPassword();
    }
    private void OctDecode()
    {
        int decodeBase = 8;
        currentPassword = ASCIIEncryption.Decode(currentPassword, decodeBase);
        AddDecodedPassword();
    }
    private void DecDecode()
    {
        int decodeBase = 10;
        currentPassword = ASCIIEncryption.Decode(currentPassword, decodeBase);
        AddDecodedPassword();
    }
    private void HexDecode()
    {
        int decodeBase = 16;
        currentPassword = ASCIIEncryption.Decode(currentPassword, decodeBase);
        AddDecodedPassword();
    }
    private void AtbashDecode()
    {
        currentPassword = AtbashCipher.DefaultEncode(currentPassword);
        AddDecodedPassword();
    }
    private void CaesarDecode()
    {
        int shift = Int32.Parse(caesarParameterInputField.text);
        currentPassword = CaesarCipher.Encode(currentPassword, CaesarCipher.DefaultBase, shift);
        AddDecodedPassword();
    }

    private void AddDecodedPassword()
    {
        CreateNewPasswordTextField(currentPassword);

        DetectionManager.CheckDetection();
        SetDetectionChanceText();

        if (!wasDetected)
        {
            NewPasswordConverted?.Invoke(currentPassword);
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
        convertedPasswordUI.DeletePasswordTriggered += DeletePassword;
        previousConvertedPasswordUIStack.Push(convertedPasswordUI);
    }

    private void DeletePassword(ConvertedPasswordUI targetConvertedPasswordUI)
    {
        while (true)
        {
            ConvertedPasswordUI currentConvertedPasswordUI = previousConvertedPasswordUIStack.Pop();
            Debug.Log(currentConvertedPasswordUI);
            currentConvertedPasswordUI.DestroySelf();
            if (currentConvertedPasswordUI == targetConvertedPasswordUI)
            {
                break;
            }
        }

        bool peeked = previousConvertedPasswordUIStack.TryPeek(out ConvertedPasswordUI convertedPasswordUI);
        currentPassword = peeked ? convertedPasswordUI.PasswordString : originalPassword;
    }

    private void RemoveAllPasswordTextFields()
    {
        while (previousConvertedPasswordUIStack.TryPop(out ConvertedPasswordUI convertedPasswordUI))
        {
            convertedPasswordUI.DestroySelf();
        }
        currentPassword = originalPassword;
    }
}
