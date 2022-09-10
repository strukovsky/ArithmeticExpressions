namespace Lab1;

public static class Calculator
{
    public static double Calculate(string expression)
    {
        try
        {
            Validator.Validate(expression);
            var result =
                new MathExpression(expression).Parse();
            return result.Calculate();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return 0.0;
    }
}