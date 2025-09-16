using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConvertedPasswordUI : MonoBehaviour
{
    [SerializeField]
    private int maxCharactersNumber;

    private const string SubstringDivisor = " | ";

    [SerializeField]
    private Transform backgroundHolder;
    [SerializeField]
    private Transform textFieldsHolder;

    [SerializeField]
    private TMP_Text convertedPasswordTextFieldPrefab;
    [SerializeField]
    private Image oneLinePasswordBackground;
    [SerializeField]
    private Image twoLinePasswordBackground;
    [SerializeField]
    private Image threeLinePasswordBackground;

    private List<TMP_Text> createdTextFieldsList;

    private RectTransform rectTransform;

    private Vector2 sizeToSet;

    private string passwordString;

    public string PasswordString => passwordString;

    public void InitializeConvertedPasswordUI(string passwordToDisplay)
    {
        rectTransform = GetComponent<RectTransform>();
        ResetTextFields();

        passwordString = passwordToDisplay;

        if (passwordToDisplay.Length == 0)
        {
            SetBackground(1);
            SetSize();

            return;
        }

        List<string> convertedPasswordList = new();

        string currentSubstring = "";
        foreach (var c in passwordToDisplay)
        {
            if (c == ' ')
            {
                convertedPasswordList.Add(currentSubstring + SubstringDivisor);
                currentSubstring = "";
            }
            else
            {
                currentSubstring += c;
            }
        }
        if (currentSubstring.Length != 0)
        {
            convertedPasswordList.Add(currentSubstring);
        }

        int textFieldsNumber = 1;
        int currentCharactersNumber = 0;

        TMP_Text currentTextField = CreateNewTextField();
        foreach (var passwordBlock in convertedPasswordList)
        {
            currentCharactersNumber += passwordBlock.Length - SubstringDivisor.Length;
            if (currentCharactersNumber > maxCharactersNumber)
            {
                string currentText = currentTextField.text;
                if (currentText.Length >= SubstringDivisor.Length)
                {
                    currentTextField.text = currentText[..^SubstringDivisor.Length];
                }

                currentTextField = CreateNewTextField();

                currentCharactersNumber = passwordBlock.Length;
                textFieldsNumber++;
            }
            currentCharactersNumber += SubstringDivisor.Length;
            currentTextField.text += passwordBlock;
        }

        string lastTextFieldText = currentTextField.text;

        // May be a temporary solution
        if (lastTextFieldText.Length >= SubstringDivisor.Length &&
            lastTextFieldText.Substring(lastTextFieldText.Length - SubstringDivisor.Length, SubstringDivisor.Length) == SubstringDivisor)
        {
            currentTextField.text = lastTextFieldText[..^SubstringDivisor.Length];
        }

        SetBackground(textFieldsNumber);
        SetSize();
    }

    private TMP_Text CreateNewTextField()
    {
        TMP_Text textField = Instantiate(convertedPasswordTextFieldPrefab.gameObject, textFieldsHolder).GetComponent<TMP_Text>();
        textField.text = string.Empty;
        createdTextFieldsList.Add(textField);
        return textField;
    }

    private void SetBackground(int linesNumber)
    {
        GameObject backgroundToInstantiate = linesNumber switch
        {
            1 => oneLinePasswordBackground.gameObject,
            2 => twoLinePasswordBackground.gameObject,
            3 => threeLinePasswordBackground.gameObject,
            _ => null
        };

        if (backgroundToInstantiate == null)
        {
            Debug.LogWarning("Error: wrong number of lines in decoded password!");
            backgroundToInstantiate = oneLinePasswordBackground.gameObject;
        }

        RectTransform backgroundRectTransform = Instantiate(backgroundToInstantiate, backgroundHolder).GetComponent<RectTransform>();
        sizeToSet = backgroundRectTransform.sizeDelta;
    }

    private void SetSize()
    {
        rectTransform.sizeDelta = sizeToSet;
    }

    public void DestroySelf()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public void ResetTextFields()
    {
        if (createdTextFieldsList != null)
        {
            foreach (var textField in createdTextFieldsList)
            {
                Destroy(textField.gameObject);
            }
        }
        createdTextFieldsList = new();
    }
}