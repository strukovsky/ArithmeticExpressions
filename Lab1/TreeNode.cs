namespace Lab1;

internal class TreeNode
{
    private readonly string _value;
    private readonly Token _tokenType;
    public TreeNode? LeftChild;
    public TreeNode? RightChild;

    public TreeNode(string value, Token tokenType)
    {
        _value = value;
        _tokenType = tokenType;
    }

    public double Calculate()
    {
        switch (_tokenType)
        {
            case Token.Operator:
            {
                var left = LeftChild!.Calculate();
                var right = RightChild!.Calculate();
                switch (_value)
                {
                    case "+":
                        return left + right;
                    case "-":
                        return left - right;
                    case "*":
                        return left * right;
                    case "/":
                        return left / right;
                    case "^":
                        return Math.Pow(left, right);
                }

                break;
            }
            case Token.Number:
                return double.Parse(_value);
            case Token.Operation:
            {
                var left = LeftChild!.Calculate();
                var right = RightChild!.Calculate();
                return new Operation(_value).Calculate(left, right);
            }
        }

        return 0;
    }

    public override string ToString()
    {
        return _value;
    }
}
