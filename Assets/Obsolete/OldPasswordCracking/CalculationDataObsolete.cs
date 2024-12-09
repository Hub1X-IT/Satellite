public class CalculationDataObsolete
{
    public CalculationDataObsolete() { }


    public CalculationDataObsolete(int result, CalculationType calculation)
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