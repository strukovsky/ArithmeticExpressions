namespace Lab1;

internal class Operation
{
    private readonly string _name;
    public static readonly string[] NamesAvailable = new string[3] {"log", "pow", "min"};

    public Operation(string name)
    {
        _name = name;
    }

    public double Calculate(double a, double b)
    {
        return _name switch
        {
            "log" => Math.Log(a, b),
            "pow" => Math.Pow(a, b),
            "min" => Math.Min(a, b),
            _ => 0
        };
    }
}
