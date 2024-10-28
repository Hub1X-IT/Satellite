public class EncryptedCharacter
{
    public int Result { get; private set; }

    public int Number1 { get; private set; }
    public int Number2 { get; private set; }
    public int Number3 { get; private set; }
    public int Number4 { get; private set; }


    // Between numbers 1 & 2
    public CalculationData.CalculationType Calculation1 { get; private set; }

    // Between numbers 2 & 3
    public CalculationData.CalculationType Calculation2 { get; private set; }

    // Between numbers 3 & 4
    public CalculationData.CalculationType Calculation3 { get; private set; }


    public string[] EncryptedCharacterStringArray { get; private set; }
    public string EncryptedCharacterString { get; private set; }


    public EncryptedCharacter(int result, int number1, int number2, int number3, int number4,
        CalculationData.CalculationType calculation1, CalculationData.CalculationType calculation2, CalculationData.CalculationType calculation3)
    {
        Result = result;

        Number1 = number1;
        Number2 = number2;
        Number3 = number3;
        Number4 = number4;

        Calculation1 = calculation1;
        Calculation2 = calculation2;
        Calculation3 = calculation3;

        CreateEncryptedCharacterStrings();
    }


    public EncryptedCharacter(CalculationData calculationDataMiddle, CalculationData calculationDataFirst, CalculationData calculationDataLast)
    {
        Result = calculationDataMiddle.Result;

        Number1 = calculationDataFirst.Value1;
        Number2 = calculationDataFirst.Value2;
        Number3 = calculationDataLast.Value1;
        Number4 = calculationDataLast.Value2;

        Calculation1 = calculationDataFirst.Calculation;
        Calculation2 = calculationDataMiddle.Calculation;
        Calculation3 = calculationDataLast.Calculation;

        CreateEncryptedCharacterStrings();
    }


    private void CreateEncryptedCharacterStrings()
    {
        EncryptedCharacterStringArray = CreateEncryptedCharacterStringArray();
        EncryptedCharacterString = CreateEncryptedCharacterString();
    }


    private string[] CreateEncryptedCharacterStringArray()
    {
        string[] stringArray = new string[7];

        stringArray[0] = Number1.ToString();
        stringArray[1] = GetCalculationString(Calculation1);
        stringArray[2] = Number2.ToString();
        stringArray[3] = GetCalculationString(Calculation2);
        stringArray[4] = Number3.ToString();
        stringArray[5] = GetCalculationString(Calculation3);
        stringArray[6] = Number4.ToString();

        return stringArray;
    }


    private string CreateEncryptedCharacterString()
    {
        string encryptedCharacterString = EncryptedCharacterStringArray[0] + " " +
            EncryptedCharacterStringArray[1] + " " +
            EncryptedCharacterStringArray[2] + " " +
            EncryptedCharacterStringArray[3] + " " +
            EncryptedCharacterStringArray[4] + " " +
            EncryptedCharacterStringArray[5] + " " +
            EncryptedCharacterStringArray[6] + " ";

        return encryptedCharacterString;
    }


    private string GetCalculationString(CalculationData.CalculationType calculation)
    {
        switch (calculation)
        {
            default:
                return string.Empty;
            case CalculationData.CalculationType.Add:
                return "+";
            case CalculationData.CalculationType.Subtract:
                return "-";
            case CalculationData.CalculationType.Multiply:
                return "*";
            case CalculationData.CalculationType.Divide:
                return "/";
        }
    }
}