public class CalculationData
{
    public CalculationData() { }


    public CalculationData(int result, CalculationType calculation)
    {
        Result = result;
        Calculation = calculation;
    }


    public enum CalculationType
    {
        None,
        Add,
        Subtract,
        Multiply,
        Divide,
    }


    public int Value1 { get; set; }
    public int Value2 { get; set; }
    public int Result { get; set; }
    public CalculationType Calculation { get; set; }
}