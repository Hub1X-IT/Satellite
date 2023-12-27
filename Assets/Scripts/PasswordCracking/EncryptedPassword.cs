public class EncryptedPassword {

    public EncryptedPassword(int result, int number1, int number2, int number3, int number4, CalculationData.Calculation calculation1, CalculationData.Calculation calculation2, CalculationData.Calculation calculation3) {
        this.result = result;
        this.number1 = number1;
        this.number2 = number2;
        this.number3 = number3;
        this.number4 = number4;
        this.calculation1 = calculation1;
        this.calculation2 = calculation2;
        this.calculation3 = calculation3;

        CreateEncryptedPasswordStrings();
    }


    public EncryptedPassword(CalculationData calculationDataMiddle, CalculationData calculationDataFirst, CalculationData calculationDataLast) {
        result = calculationDataMiddle.GetResult();

        number1 = calculationDataFirst.GetValue1();
        number2 = calculationDataFirst.GetValue2();
        number3 = calculationDataLast.GetValue1();
        number4 = calculationDataLast.GetValue2();

        calculation1 = calculationDataFirst.GetCalculation();
        calculation2 = calculationDataMiddle.GetCalculation();
        calculation3 = calculationDataLast.GetCalculation();

        CreateEncryptedPasswordStrings();
    }


    private void CreateEncryptedPasswordStrings() {
        encryptedPasswordStringArray = CreateEncryptedPasswordStringArray();
        encryptedPasswordString = CreateEncryptedPasswordString();
    }


    private string[] CreateEncryptedPasswordStringArray() {
        string[] stringArray = new string[7];

        stringArray[0] = number1.ToString();
        stringArray[1] = GetCalculationString(calculation1);
        stringArray[2] = number2.ToString();
        stringArray[3] = GetCalculationString(calculation2);
        stringArray[4] = number3.ToString();
        stringArray[5] = GetCalculationString(calculation3);
        stringArray[6] = number4.ToString();

        return stringArray;
    }


    private string CreateEncryptedPasswordString() {
        string encryptedPasswordString = encryptedPasswordStringArray[0] + " " +
            encryptedPasswordStringArray[1] + " " +
            encryptedPasswordStringArray[2] + " " +
            encryptedPasswordStringArray[3] + " " +
            encryptedPasswordStringArray[4] + " " +
            encryptedPasswordStringArray[5] + " " +
            encryptedPasswordStringArray[6] + " ";

        return encryptedPasswordString;
    }


    private string GetCalculationString(CalculationData.Calculation calculation) {
        switch (calculation) {
            default:
                return string.Empty;
            case CalculationData.Calculation.Add:
                return "+";
            case CalculationData.Calculation.Subtract:
                return "-";
            case CalculationData.Calculation.Multiply:
                return "*";
            case CalculationData.Calculation.Divide:
                return "/";
        }
    }


    private int result;

    private int number1;
    private int number2;
    private int number3;
    private int number4;

    private CalculationData.Calculation calculation1; // Between numbers 1 & 2
    private CalculationData.Calculation calculation2; // Between numbers 2 & 3
    private CalculationData.Calculation calculation3; // Between numbers 3 & 4


    private string[] encryptedPasswordStringArray;
    private string encryptedPasswordString;


    public int GetResult() { return result; }
    public int GetNumber1() { return number1; }
    public int GetNumber2() { return number2; }
    public int GetNumber3() { return number3; }
    public int GetNumber4() { return number4; }
    public CalculationData.Calculation GetCalculation1() { return calculation1; }
    public CalculationData.Calculation GetCalculation2() { return calculation2; }
    public CalculationData.Calculation GetCalculation3() { return calculation3; }
    public string GetEncryptedPasswordString() { return encryptedPasswordString; }
    public string[] GetEncryptedPasswordStringArray() { return encryptedPasswordStringArray; }
}