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
    private Button caesarButton;

    [SerializeField]
    private TMP_InputField caesarParameterInputField;

    [SerializeField]
    private Button atbashButton;

    private string currentPassword;

    private string originalPassword;

    private Stack<ConvertedPasswordUI> previousConvertedPasswordUIStack;

    [SerializeField]
    private TMP_Text detectionChance;
    private DetectionManager detectionManager;
    private const string DETECTION_CHANCE = "Detection Chance: ";
    private int detectionChanceNumber;

    public void InitializePasswordCrackingApp(string appName)
    {
        monitorAppUI = GetComponent<MonitorAppUI>();
        monitorAppUI.SetAppName(appName);

        detectionManager = FindAnyObjectByType<DetectionManager>();

        InitializePasswordCracking();
        SetDetectionChanceText();
    }

    private void InitializePasswordCracking()
    {
        previousConvertedPasswordUIStack = new();

        originalPassword = currentPassword = "";

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

        decButton.onClick.AddListener(() =>
        {
            currentPassword = ASCIIEncryption.Decode(currentPassword, 10);
            CreateNewPasswordTextField(currentPassword);
            detectionManager.CheckDetection();
            SetDetectionChanceText();
        });
        hexButton.onClick.AddListener(() =>
        {
            currentPassword = ASCIIEncryption.Decode(currentPassword, 16);
            CreateNewPasswordTextField(currentPassword);
            detectionManager.CheckDetection();
            SetDetectionChanceText();
        });
        caesarButton.onClick.AddListener(() =>
        {
            int shift = Int32.Parse(caesarParameterInputField.text);
            currentPassword = CaesarCipher.Encode(currentPassword, CaesarCipher.DefaultBase, shift);
            CreateNewPasswordTextField(currentPassword);
            detectionManager.CheckDetection();
            SetDetectionChanceText();
        });
        binButton.onClick.AddListener(() =>
        {
            currentPassword = ASCIIEncryption.Decode(currentPassword, 2);
            CreateNewPasswordTextField(currentPassword);
            detectionManager.CheckDetection();
            SetDetectionChanceText();
        });
        octButton.onClick.AddListener(() =>
        {
            currentPassword = ASCIIEncryption.Decode(currentPassword, 8);
            CreateNewPasswordTextField(currentPassword);
            detectionManager.CheckDetection();
            SetDetectionChanceText();
        });
        atbashButton.onClick.AddListener(() =>
        {
            currentPassword = AtbashCipher.DefaultEncode(currentPassword);
            CreateNewPasswordTextField(currentPassword);
            detectionManager.CheckDetection();
            SetDetectionChanceText();
        });
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

    private void SetDetectionChanceText()
    {
        detectionChanceNumber = -(detectionManager.detectionChance - 100);
        detectionChance.text = DETECTION_CHANCE + detectionChanceNumber.ToString() + "%";
    }
}
