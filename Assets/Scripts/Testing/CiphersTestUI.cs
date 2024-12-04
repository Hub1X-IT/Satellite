using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CiphersTestUI : MonoBehaviour
{
    [SerializeField]
    private GameObject convertedPasswordPrefab;

    [SerializeField]
    private Transform convertedPasswordsHolder;

    [SerializeField]
    private TMP_InputField inputField;

    [SerializeField]
    private Button decButton;

    [SerializeField]
    private Button hexButton;

    [SerializeField]
    private Button caesarButton;

    [SerializeField]
    private TMP_InputField caesarParameterInputField;

    [SerializeField]
    private Button binButton;

    [SerializeField]
    private Button octButton;

    [SerializeField]
    private Button atbashButton;

    [SerializeField]
    private Button previousPasswordButton;

    private string currentPassword;

    private Stack<TMP_Text> previousPasswordTextFieldsStack;

    private void Awake()
    {
        previousPasswordTextFieldsStack = new();

        inputField.onEndEdit.AddListener((text) =>
        {
            currentPassword = text;
            RemoveAllPasswordTextFields();
        });

        decButton.onClick.AddListener(() =>
        {
            currentPassword = ASCIIEncryption.Decode(currentPassword, 10);
            CreateNewPasswordTextField(currentPassword);
        });
        hexButton.onClick.AddListener(() =>
        {
            currentPassword = ASCIIEncryption.Decode(currentPassword, 16);
            CreateNewPasswordTextField(currentPassword);
        });
        caesarButton.onClick.AddListener(() =>
        {
            int shift = Int32.Parse(caesarParameterInputField.text);
            currentPassword = CaesarCipher.Encode(currentPassword, CaesarCipher.DefaultBase, shift);
            CreateNewPasswordTextField(currentPassword);
        });
        binButton.onClick.AddListener(() =>
        {
            currentPassword = ASCIIEncryption.Decode(currentPassword, 2);
            CreateNewPasswordTextField(currentPassword);
        });
        octButton.onClick.AddListener(() =>
        {
            currentPassword = ASCIIEncryption.Decode(currentPassword, 8);
            CreateNewPasswordTextField(currentPassword);
        });
        atbashButton.onClick.AddListener(() =>
        {
            currentPassword = AtbashCipher.DefaultEncode(currentPassword);
            CreateNewPasswordTextField(currentPassword);
        });

        previousPasswordButton.onClick.AddListener(() =>
        {
            currentPassword = RemoveLastPasswordTextField();
        });
    }

    private void CreateNewPasswordTextField(string newPassword)
    {
        GameObject convertedPasswordObject = Instantiate(convertedPasswordPrefab, convertedPasswordsHolder);
        convertedPasswordObject.SetActive(true);
        TMP_Text convertedPasswordTextField = convertedPasswordObject.GetComponent<TMP_Text>();
        convertedPasswordTextField.text = newPassword;
        // Debug.Log(newPassword);
        previousPasswordTextFieldsStack.Push(convertedPasswordTextField);

        Debug.Log(currentPassword);
    }

    private string RemoveLastPasswordTextField()
    {
        if (!previousPasswordTextFieldsStack.TryPop(out TMP_Text textField))
        {
            return currentPassword;
        }

        textField.gameObject.SetActive(false);
        Destroy(textField.gameObject);

        if (!previousPasswordTextFieldsStack.TryPeek(out TMP_Text previousTextField))
        {
            return inputField.text;
        }

        return previousTextField.text;
    }

    private void RemoveAllPasswordTextFields()
    {
        while (previousPasswordTextFieldsStack.Count > 0)
        {
            GameObject textFieldGO = previousPasswordTextFieldsStack.Pop().gameObject;
            textFieldGO.SetActive(false);
            Destroy(textFieldGO);
        }
    }
}