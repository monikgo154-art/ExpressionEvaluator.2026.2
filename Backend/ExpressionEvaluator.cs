
namespace Backend;

public static class ExpressionEvaluator
{
    public static double Evalute(string infix) => EvalutePostfix(ToPostfix(infix));

    private static string ToPostfix(string infix)
    {
        var posfix = string.Empty;
        var stack = new Stack<string>();
        string num = "";
        foreach (var item in infix)
        {

            if (!IsOperator(item))
            {
                num += item;
            }
            else
            {
                if (num != "")
                {
                    posfix += num + " ";
                    num = "";
                }
                if (item == ')')
                {
                    var ope = stack.Pop();
                    while (ope != "(")
                    {
                        posfix += ope + " ";
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
                        if (PriorityInfix(item.ToString()) > PriorityStack(stack.Peek()))
                        {
                            stack.Push(item.ToString());
                        }
                        else
                        {
                            posfix += stack.Pop() + " ";
                            stack.Push(item.ToString());
                        }
                    }
                }


            }
        }
        if (num != "")
        {
            posfix += num + " ";
        }


        while (stack.Count != 0)

        {
            posfix += stack.Pop() + " ";
        }
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

        foreach (var item in postfix.Split(' ', StringSplitOptions.RemoveEmptyEntries))
        {
            if (IsOperator(item[0]))
            {
                var ope2 = stack.Pop();
                var ope1 = stack.Pop();
                stack.Push(Calculate(ope1, ope2, item[0]));
            }
            else
            {
                stack.Push(double.Parse(item));
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