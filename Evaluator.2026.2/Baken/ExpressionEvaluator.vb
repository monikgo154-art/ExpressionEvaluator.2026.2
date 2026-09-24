using System.Globalization;
Namespace ExpressionEvaluator.Core;

Public Class Evaluator
{
    Public Static Double Evaluate(String infix)
    {
        var postfix = InfixToPostfix(infix);
        Return EvaluatePostfix(postfix);  
    }

    Private Static String InfixToPostfix(String infix)
    {
        var output = "";
        var stack = New Stack < Char > ();
        var number = "";

        foreach (var c in infix.Replace(" ", ""))
        {
            
            If (Char.IsDigit(c) || c == '.')
            {
                number += c;
            }
            Else
            {
                
                If (number!= "")
                {
                    output += number + " ";
                    number = "";
                }

                If (c == '(')
                {
                    stack.Push(c);
                }
                ElseIf (c == ')')
                {
                    while (stack.Peek() != '(')
                        output += stack.Pop() + " ";
                    stack.Pop();
                }
                ElseIf (IsOperator(c))
                {
                 
                    While (stack.Count > 0 &&
                           PriorityStack(stack.Peek()) >= PriorityInfix(c))
                    {
                        output += stack.Pop() + " ";
                    }
                    stack.Push(c);
                }
            }
        }

        
        If (number!= "")
            output += number + " ";

        
        While (stack.Count > 0)
            output += stack.Pop() + " ";

        Return output.Trim();
    }

    Private Static Double EvaluatePostfix(String postfix)
    {
        var stack = New Stack < Double > ();
        var tokens = postfix.Split(' ');

        foreach (var token in tokens)
        {
            
            If (Double.TryParse(token, NumberStyles.Any, CultureInfo.InvariantCulture, out Double num))
            {
                stack.Push(num);
            }
            Else
            {
                
                var b = stack.Pop();
                var a = stack.Pop();

                stack.Push(token switch
                {
                    "+" => a + b,
                    "-" => a - b,
                    "*" => a * b,
                    "/" => a / b,
                    "^" => Math.Pow(a, b),
                    _ => throw New Exception("Error de sintaxis")
                });
            }
        }

        Return stack.Pop();
    }

    Private Static int PriorityStack(Char item) => item switch
    {
        '^' => 3,
        '*' => 2,
        '/' => 2,
        '+' => 1,
        '-' => 1,
        '(' => 0,
        _ => 0
    };

    Private Static int PriorityInfix(Char item) => item switch
    {
        '^' => 4,
        '*' => 2,
        '/' => 2,
        '+' => 1,
        '-' => 1,
        '(' => 5,
        _ => 0
    };

    Private Static bool IsOperator(Char c) => "+-*/^".Contains(c);
}