public class CalculationData {


    public CalculationData() { }
    

    public CalculationData(int result, Calculation calculation) {
        this.result = result;
        this.calculation = calculation;
    }


    public enum Calculation {        
        None,
        Add,
        Subtract,
        Multiply,
        Divide,
    }


    private int value1;
    private int value2;
    private int result;
    private Calculation calculation;


    public int GetValue1() { return value1; }
    public int GetValue2() { return value2; }
    public int GetResult() { return result; }
    public Calculation GetCalculation() { return calculation; }


    public void SetValue1(int value) { value1 = value; }
    public void SetValue2(int value) { value2 = value; }
    public void SetResult(int value) { result = value; }
    public void SetCalculation(Calculation calculation) { this.calculation = calculation; }
}