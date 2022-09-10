namespace Lab1;

internal class MathExpression
{
    private const string Operators = "+-/*^";
    private readonly string _expression;
    private TreeNode? _root;

    public MathExpression(string expression)
    {
        _expression = expression.Trim();
    }

    private int GetIndexOfRootOperator()
    {
        var parenthesisCount = 0;
        for (var i = 0; i < _expression.Length; i++)
        {
            var character = _expression[i];
            if (i > 0 && parenthesisCount == 0 && Operators.Contains(character))
            {
                return i;
            }

            switch (character)
            {
                case '(':
                    parenthesisCount++;
                    break;
                case ')':
                    parenthesisCount--;
                    break;
            }
        }

        return -1;
    }

    private string PrepareSubExpression(int startIndex, int endIndex)
    {
        var result = _expression.Substring(startIndex, endIndex - startIndex).Trim();
        var removedOpenParenthesis = false;
        if (result.StartsWith("("))
        {
            removedOpenParenthesis = true;
            result = result.Substring(1, result.Length - 1).Trim();
        }

        if (result.EndsWith(")"))
        {
            if (removedOpenParenthesis)
            {
                result = result.Substring(0, result.Length - 1).Trim();
            }
        }

        return result.Trim();
    }

    public TreeNode Parse()
    {
        var rootIndex = GetIndexOfRootOperator();
        if (rootIndex == -1)
        {
            var _ = 0.0;
            if (double.TryParse(_expression, out _))
            {
                _root = new TreeNode(_expression, Token.Number);
            }
            else
            {
                var commaIndex = _expression.IndexOf(",", StringComparison.Ordinal);
                var openParenthesisIndex = _expression.IndexOf("(", StringComparison.Ordinal);
                var operation = _expression.Substring(0, openParenthesisIndex);
                if (!Operation.NamesAvailable.Contains(operation))
                {
                    throw new Exception($"Operation \"{operation}\" is unsupported");
                }

                var firstArgument =
                    PrepareSubExpression(openParenthesisIndex + 1, commaIndex);
                var secondArgument = PrepareSubExpression(commaIndex + 1, _expression.Length - 1);
                _root = new TreeNode(operation, Token.Operation)
                {
                    LeftChild = new MathExpression(firstArgument).Parse(),
                    RightChild = new MathExpression(secondArgument).Parse()
                };
            }
        }
        else
        {
            var left = PrepareSubExpression(0, rootIndex);
            var right = PrepareSubExpression(rootIndex + 1, _expression.Length);
            _root = new TreeNode(_expression[rootIndex].ToString(), Token.Operator)
            {
                LeftChild = new MathExpression(left).Parse(),
                RightChild = new MathExpression(right).Parse()
            };
        }
        return _root;
    }
}