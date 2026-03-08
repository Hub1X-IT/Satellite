using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PasswordCrackingAppUI : MonoBehaviour
{
    [Serializable]
    public class GuidebookLookupButton
    {
        public Button LookupButton;
        public int PageNumber;
    }

    [SerializeField]
    private GameEventSO onCorrectPasswordDecodedGameEvent;

    private MonitorAppUI monitorAppUI;

    private Monitor monitor;

    [SerializeField]
    private ConvertedPasswordUI convertedPasswordPrefab;
    [SerializeField]
    private Transform convertedPasswordsHolder;
    [SerializeField]
    private ConvertedPasswordUI decompressedPasswordUI;
    [SerializeField]
    private TMP_InputField inputField;

    [SerializeField]
    private GameObject decodingMessagePrefab;
    [SerializeField]
    private GameObject errorMessagePrefab;
    private const float DecodingMessageShowTime = 1f;

    private GameObject currentDecodingMessageObject;
    private GameObject currentErrorMessageObject;

    // private bool shouldShowDecodingMessage;
    // private bool shouldShowErrorMessage;
    // private float decodingMessageTimer;

    [SerializeField]
    private CopyPasteMenuUI pasteMenuUI;

    [SerializeField]
    private Button resetPasswordCrackingButton;

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

    [SerializeField]
    private GuidebookLookupButton[] guidebookLookupButtons;

    private string decompressedPassword;
    private Stack<ConvertedPasswordUI> previousConvertedPasswordUIStack;
    private EncryptedPassword currentEncryptedPassword;
    private PasswordEncryption.EncryptionStep[] passwordEncryptionSteps;
    private int encryptionStepIndex;

    [SerializeField]
    private TMP_Text detectionChanceTextField;
    private const string DetectionChanceText = "Detection Chance: ";

    public void InitializePasswordCrackingApp(string appName)
    {
        monitorAppUI = GetComponent<MonitorAppUI>();
        monitorAppUI.SetAppName(appName);
        monitorAppUI.DestroyOnClose = false;

        monitor = GetComponentInParent<Monitor>();

        InitializePasswordCracking();
        SetDetectionChanceText(DetectionManager.Instance.CurrentDetectionChance);

        pasteMenuUI.InitializeCopyPasteMenuUI(CopyPasteMenuUI.MenuFunction.PasteMenu, inputField);

        PowerManager.Instance.OnPowerStateChanged += (isPowerEnabled) =>
        {
            if (!isPowerEnabled && monitorAppUI != null)
            {
                monitorAppUI.DestroyOnClose = true;
                monitorAppUI.CloseApp();
            }
        };
        DetectionManager.Instance.OnDetectionChanceChanged += (newDetectionChance) =>
        {
            SetDetectionChanceText(newDetectionChance);
        };

        // shouldShowDecodingMessage = false;
        // shouldShowErrorMessage = false;

        pasteMenuUI.SetCopyPasteMenuEnabled(false);
    }

    private void InitializePasswordCracking()
    {
        previousConvertedPasswordUIStack = new();

        decompressedPassword = string.Empty;
        decompressedPasswordUI.gameObject.SetActive(false);

        EnableApp();
    }

    private void EnableApp()
    {
        resetPasswordCrackingButton.onClick.AddListener(ResetPasswordCracking);
        inputField.onSelect.AddListener(InputField_OnSelect);
        inputField.onValueChanged.AddListener(InputField_OnValueChanged);
        inputField.onValidateInput += InputField_OnValidateInput;

        undoAllStepsButton.onClick.AddListener(UndoAllSteps);
        undoLastStepButton.onClick.AddListener(UndoLastStep);

        binButton.onClick.AddListener(BinDecode);
        octButton.onClick.AddListener(OctDecode);
        decButton.onClick.AddListener(DecDecode);
        hexButton.onClick.AddListener(HexDecode);
        atbashButton.onClick.AddListener(AtbashDecode);
        caesarButton.onClick.AddListener(CaesarDecode);

        foreach (var guidebookLookupButton in guidebookLookupButtons)
        {
            guidebookLookupButton.LookupButton.onClick.AddListener(() => LookupPageInGuidebook(guidebookLookupButton.PageNumber));
        }
    }

    private void SetButtonsEnabled(bool enabled)
    {
        resetPasswordCrackingButton.interactable = enabled;
        undoAllStepsButton.interactable = enabled;
        undoLastStepButton.interactable = enabled;

        binButton.interactable = enabled;
        octButton.interactable = enabled;
        decButton.interactable = enabled;
        hexButton.interactable = enabled;
        atbashButton.interactable = enabled;
        caesarButton.interactable = enabled;
    }

    private void InputField_OnSelect(string _)
    {
        pasteMenuUI.SetCopyPasteMenuEnabled(true);
    }
    private void InputField_OnValueChanged(string newValue)
    {
        if (newValue.Length == TextCompressor.CompressedTextLength)
        {
            DecompressPassword(newValue);
        }
        else
        {
            decompressedPasswordUI.gameObject.SetActive(false);
        }
    }
    private char InputField_OnValidateInput(string text, int charIndex, char addedChar)
    {
        if (!char.IsLetter(addedChar))
        {
            return '\0';
        }
        return char.ToUpper(addedChar);
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

            decompressedPasswordUI.InitializeConvertedPasswordUI(decompressedPassword, false);
            decompressedPasswordUI.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("Decompression failed.");
            decompressedPasswordUI.gameObject.SetActive(false);
        }
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

        TryDestroyErrorMessageObject();
        TryDestroyDecodingMessageObject();

        if (usedCipher == currentEncryptionStep.usedCipherType)
        {
            Debug.Log("Decoding step correct");
            encryptionStepIndex--;
            string convertedPassword = currentEncryptionStep.PreviousPasswordState;

            StartCoroutine(ShowDecodingMessageAndConvertedPassword(false, convertedPassword, encryptionStepIndex == -1));

            // shouldShowDecodingMessage = true;
            // shouldShowErrorMessage = true;
            // decodingMessageTimer = DecodingMessageShowTime;

            // CreateNewPasswordTextField(convertedPassword);

            if (encryptionStepIndex == -1)
            {
                // Player guessed the correct password.
            }
        }
        else
        {
            Debug.Log("Decoding step incorrect");

            // shouldShowDecodingMessage = true;
            // shouldShowErrorMessage = true;
            // decodingMessageTimer = DecodingMessageShowTime;

            StartCoroutine(ShowDecodingMessageAndConvertedPassword(true, "", false));

            DetectionManager.Instance.CheckDetection();
        }
    }

    // Probably temporary
    private IEnumerator ShowDecodingMessageAndConvertedPassword(bool showErrorMessage, string convertedPassword, bool isFinalStep)
    {
        currentDecodingMessageObject = Instantiate(decodingMessagePrefab, convertedPasswordsHolder);
        SetButtonsEnabled(false);
        yield return new WaitForSeconds(DecodingMessageShowTime);
        if (TryDestroyDecodingMessageObject())
        {
            if (showErrorMessage)
            {
                currentErrorMessageObject = Instantiate(errorMessagePrefab, convertedPasswordsHolder);
            }
            else
            {
                CreateNewPasswordTextField(convertedPassword, isFinalStep);

                if (isFinalStep && onCorrectPasswordDecodedGameEvent != null)
                {
                    onCorrectPasswordDecodedGameEvent.RaiseEvent();
                }
            }
        }
        SetButtonsEnabled(true);
    }

    private void SetDetectionChanceText(int detectionChance)
    {
        detectionChanceTextField.text = DetectionChanceText + detectionChance + "%";
    }

    private void CreateNewPasswordTextField(string newPassword, bool isCorrectPassword)
    {
        ConvertedPasswordUI convertedPasswordUI = Instantiate(convertedPasswordPrefab.gameObject,
            convertedPasswordsHolder).GetComponent<ConvertedPasswordUI>();

        convertedPasswordUI.InitializeConvertedPasswordUI(newPassword, isCorrectPassword);
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
        TryDestroyDecodingMessageObject();
        TryDestroyErrorMessageObject();
        RemoveAllPasswordTextFields();
        ResetPasswordEncryptionSteps();
    }

    private void UndoLastStep()
    {
        TryDestroyDecodingMessageObject();

        if (!TryDestroyErrorMessageObject() && previousConvertedPasswordUIStack.TryPop(out ConvertedPasswordUI currentConvertedPasswordUI))
        {
            currentConvertedPasswordUI.DestroySelf();
            encryptionStepIndex++;
        }
    }

    private void LookupPageInGuidebook(int pageNumber)
    {
        monitor.ComputerComponent.ChangeCurrentComputer(monitor.ParentDesk.Guidebook.ComputerComponent);
        monitor.ParentDesk.Guidebook.GuidebookInterface.ChangeToPage(pageNumber);
    }

    private bool TryDestroyDecodingMessageObject()
    {
        if (currentDecodingMessageObject != null)
        {
            Destroy(currentDecodingMessageObject);
            currentDecodingMessageObject = null;
            return true;
        }
        return false;
    }

    private bool TryDestroyErrorMessageObject()
    {
        if (currentErrorMessageObject != null)
        {
            Destroy(currentErrorMessageObject);
            currentErrorMessageObject = null;
            return true;
        }
        return false;
    }
}
