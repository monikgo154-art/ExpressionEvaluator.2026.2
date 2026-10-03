
namespace Backend;

public static class ExpressionEvaluator
{
    public static double Evalute(string infix) => EvalutePostfix(ToPostfix(infix));

    private static string ToPostfix(string infix)
    {
        var posfix = string.Empty;
        var stack = new Stack<string>();
        foreach (var item in infix)
        {
            if (IsOperator(item))
            {
                if (item == ')')
                {
                    var ope = stack.Pop();
                    while (ope != "(")
                    {
                        posfix += ope;
                        ope = stack.Pop();
                    }
                }
                else
                {
                    if (stack.Count == 0)
                    {
                        stack.Push(item.ToString());
                    }
                    else
                    {
                        if (PriorityInfix(item) > PriorityStack(stack.Peek()))
                        {
                            stack.Push(item.ToString());
                        }
                        else
                        {
                            posfix += stack.Pop();
                            stack.Push(item.ToString());
                        }
                    }
                }
            }
            else
            {
                posfix += item;
            }
        }
        do
        {
            posfix += stack.Pop();
        } while (stack.Count != 0);
        return posfix;
    }

    private static int PriorityStack(string op) => op switch
    {
        "^" => 3,
        "*" => 2,
        "/" => 2,
        "+" => 1,
        "-" => 1,
        "(" => 0,
        _ => throw new Exception("Invalid expression."),
    };

    private static int PriorityInfix(string op) => op switch
    {
        "^" => 4,
        "*" => 2,
        "/" => 2,
        "+" => 1,
        "-" => 1,
        "(" => 5,
        _ => throw new Exception("Invalid expression."),
    };

    private static bool IsOperator(char item) => item == '^' || item == '*' || item == '/' || item == '+' || item == '-' || item == '(' || item == ')';

    private static double EvalutePostfix(string postfix)
    {
        var stack = new Stack<double>();
        foreach (var item in postfix)
        {
            if (IsOperator(item))
            {
                var ope2 = stack.Pop();
                var ope1 = stack.Pop();
                stack.Push(Calculate(ope1, ope2, item));
            }
            else
            {
                stack.Push(char.GetNumericValue(item));
            }
        }
        return stack.Pop();
    }

    private static double Calculate(double ope1, double ope2, char item) => item switch
    {
        '*' => ope1 * ope2,
        '/' => ope1 / ope2,
        '+' => ope1 + ope2,
        '-' => ope1 - ope2,
        '^' => Math.Pow(ope1, ope2),
        _ => throw new Exception("Invalid expression."),
    };
}