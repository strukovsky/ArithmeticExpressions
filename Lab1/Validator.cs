using System.Reflection;
using System.Text.RegularExpressions;

namespace Lab1;

public static class Validator
{
    private static readonly string Operations = string.Join("", Operation.NamesAvailable);

    private static bool HasInappropriateCharacters(string expression)
    {
        return new Regex($"[^0-9.+-/*^a-zA-Z()\\s]+").IsMatch(expression);
    }

    private static bool HasUnmatchedParenthesis(string expression)
    {
        var stack = 0;
        foreach (var character in expression)
        {
            switch (character)
            {
                case '(':
                    stack++;
                    break;
                case ')':
                    stack--;
                    break;
            }
        }

        return stack != 0;
    }

    private static bool HasUnregisteredOperations(string expression)
    {
        var regex = new Regex("[^a-zA-Z]+");
        var usedOperations = regex.Split(expression);
        return usedOperations.Where(usedOperation => usedOperation.Length > 0)
            .Any(usedOperation => !Operation.NamesAvailable.Contains(usedOperation));
    }

    private static bool HasDoubleOperatorProblem(string expression)
    {
        return new Regex("[+-/*^]+\\s*[+-/*^]+").IsMatch(expression);
    }

    public static void Validate(string expression)
    {
        if (HasInappropriateCharacters(expression))
            throw new Exception($"Expression {expression} has bad symbols in it");
        if (HasUnmatchedParenthesis(expression))
            throw new Exception($"Expression {expression} has unmatched parenthesis");
        if (HasUnregisteredOperations(expression))
            throw new Exception($"Expression {expression} has unregistered operations");
        if (HasDoubleOperatorProblem(expression))
            throw new Exception($"Expression {expression} has double operators");
    }
}
