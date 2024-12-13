using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PasswordCrackingAppUI : MonoBehaviour
{
    // UI objects referenced in this script should not have listeners added to their events in any other scripts.

    private MonitorAppUI monitorAppUI;

    [SerializeField]
    private ConvertedPasswordUI convertedPasswordPrefab;
    [SerializeField]
    private Transform convertedPasswordsHolder;
    [SerializeField]
    private TMP_InputField inputField;
    [SerializeField]
    private Button decompressButton;
    [SerializeField]
    private Button goBackToInputFieldButton;

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

    public void InitializePasswordCrackingApp(string appName)
    {
        monitorAppUI = GetComponent<MonitorAppUI>();
        monitorAppUI.SetAppName(appName);

        InitializePasswordCracking();
        SetDetectionChanceText();

        DetectionManager.DetectionOccured += DisableApp;
        DetectionManager.DetectionRemoved += EnableApp;
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
        inputField.onEndEdit.AddListener((text) =>
        {
            originalPassword = currentPassword = text;
            RemoveAllPasswordTextFields();
        });

        decompressButton.onClick.AddListener(() =>
        {
            string compressedPassword = inputField.text;
            if (TextCompressor.TryGetDecompressedText(compressedPassword, out string decompressedPassword))
            {
                originalPassword = currentPassword = inputField.text = decompressedPassword;
                RemoveAllPasswordTextFields();
            }
            else
            {
                Debug.Log("Decompression failed.");
            }
        });
        goBackToInputFieldButton.onClick.AddListener(() =>
        {
            currentPassword = originalPassword;
            RemoveAllPasswordTextFields();
        });

        binButton.onClick.AddListener(() =>
        {
            currentPassword = ASCIIEncryption.Decode(currentPassword, 2);
            SubmitPassword();
        });
        octButton.onClick.AddListener(() =>
        {
            currentPassword = ASCIIEncryption.Decode(currentPassword, 8);
            SubmitPassword();
        });
        decButton.onClick.AddListener(() =>
        {
            currentPassword = ASCIIEncryption.Decode(currentPassword, 10);
            SubmitPassword();
        });
        hexButton.onClick.AddListener(() =>
        {
            currentPassword = ASCIIEncryption.Decode(currentPassword, 16);
            SubmitPassword();
        });
        atbashButton.onClick.AddListener(() =>
        {
            currentPassword = AtbashCipher.DefaultEncode(currentPassword);
            SubmitPassword();
        });
        caesarButton.onClick.AddListener(() =>
        {
            int shift = Int32.Parse(caesarParameterInputField.text);
            currentPassword = CaesarCipher.Encode(currentPassword, CaesarCipher.DefaultBase, shift);
            SubmitPassword();
        });
    }

    private void DisableApp()
    {
        inputField.onEndEdit.RemoveAllListeners();

        decompressButton.onClick.RemoveAllListeners();
        goBackToInputFieldButton.onClick.RemoveAllListeners();

        binButton.onClick.RemoveAllListeners();
        octButton.onClick.RemoveAllListeners();
        decButton.onClick.RemoveAllListeners();
        hexButton.onClick.RemoveAllListeners();
        atbashButton.onClick.RemoveAllListeners();
        caesarButton.onClick.RemoveAllListeners();
    }

    // May not be the best name
    private void SubmitPassword()
    {
        CreateNewPasswordTextField(currentPassword);

        DetectionManager.CheckDetection();

        SetDetectionChanceText();
    }

    private void SetDetectionChanceText()
    {
        int detectionChanceNumber = -(DetectionManager.CurrentDetectionChance - 100);
        detectionChanceTextField.text = DetectionChanceText + detectionChanceNumber.ToString() + "%";
    }

    private void CreateNewPasswordTextField(string newPassword)
    {
        ConvertedPasswordUI convertedPasswordUI = Instantiate(convertedPasswordPrefab.gameObject,
            convertedPasswordsHolder).GetComponent<ConvertedPasswordUI>();

        convertedPasswordUI.InitializeConvertedPasswordUI(newPassword);

        convertedPasswordUI.GoBackToPasswordTriggered += GoBackToPassword;

        previousConvertedPasswordUIStack.Push(convertedPasswordUI);
    }

    private void GoBackToPassword(ConvertedPasswordUI targetConvertedPasswordUI)
    {
        while (true)
        {
            ConvertedPasswordUI convertedPasswordUI = previousConvertedPasswordUIStack.Peek();
            if (convertedPasswordUI == targetConvertedPasswordUI)
            {
                currentPassword = previousConvertedPasswordUIStack.Peek().ConvertedPassword;
                break;
            }
            convertedPasswordUI.DestroySelf();
            previousConvertedPasswordUIStack.Pop();
        }
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
