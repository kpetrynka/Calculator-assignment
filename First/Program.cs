using System.Globalization;
using Queue = First.Queue;
using Stack = First.Stack;

//Input the calculation
var stage = true;
while (stage)
{
    Console.WriteLine("Enter expression: ");
    var expression = Console.ReadLine() ?? throw new InvalidOperationException();
    var token = ToToken(expression);
    var transformed = PostFix(token);
    var result = CalculationPerformed(transformed);
    Console.WriteLine(result);
    Console.WriteLine("Try again? +/-");
    string answer = Console.ReadLine() ?? throw new InvalidOperationException();
    if (answer != "+")
    {
        stage = false;
    }
}

//do the tokens;
Queue ToToken(string inputted)
{
    var n = "";
    var function = "";
    var output = new Queue();
    foreach (char i in inputted)
    {
        if (char.IsDigit(i))
        {
            n += i.ToString();
        }
        
        else if (char.IsLetter(i))
        {
            function += i.ToString();
        }
        else if (char.IsWhiteSpace(i))
        {
            if (function.Length > 0)
            { 
                output.Add(function);
                function = "";
            }
            else if (n.Length > 0)
            { 
                output.Add(n);
                n = ""; 
            }
        }
        else
        {
            if (function.Length > 0)
            { 
                output.Add(function);
                function = "";
            }
            else if (n.Length > 0)
            { 
                output.Add(n);
                n = ""; 
            }
            output.Add(i.ToString());
        }
    }

    if (n.Length > 0)
    {
        output.Add(n);
    }
    return output;
}


Queue PostFix(Queue tokens)
{ 
    Dictionary<string, int> priority = new Dictionary<string, int>() //we will need it for polish notation
    {
        ["+"] = 1,
        ["-"] = 1,
        ["*"] = 2,
        ["/"] = 2,
        ["^"] = 2, 
    };

    var operatorStack = new Stack();
    var output = new Queue();
    while (!tokens.IsEmpty())
    {
        var token = tokens.Out();
        var checkIfNumber = double.TryParse(token, NumberStyles.Any, CultureInfo.InvariantCulture, out _);
        if (checkIfNumber)
        {
            output.Add(token);
        }
        else if (token is "sin" or "cos" or "tg")
        {
            operatorStack.Push(token);
        }
        else if (priority.TryGetValue(token, out var tokenPriority))
        {
            if (operatorStack.IsEmpty())
            {
                operatorStack.Push(token);
            }
            else while (!operatorStack.IsEmpty() && priority[operatorStack.Peek()] > tokenPriority ||
                   priority[operatorStack.Peek()] == tokenPriority)
            {
                if (operatorStack.Peek() != "(")
                    output.Add(operatorStack.Pop());
                else break;
            }
        }
        else switch (token)
        {
            case "(":
                operatorStack.Push(token);
                break;
            case ")":
            {
                while (operatorStack.Peek() != "(")
                {
                    output.Add(operatorStack.Pop());
                }
                operatorStack.Pop();
                if (operatorStack.Peek() is "sin" or "cos" or "tg")
                {
                    output.Add(operatorStack.Pop());
                }
                break;
            }
        }
    }
    
    while (!operatorStack.IsEmpty())
    {
        output.Add(operatorStack.Pop());
    }
    return output;
}

string CalculationPerformed(Queue postFixed)
{
    var numbers = new Stack();
    while (!postFixed.IsEmpty())
    {
        var token = postFixed.Out();
        if (int.TryParse(token, out var num))
        {
            numbers.Push(num.ToString());
        }
        else if (token is "sin" or "cos" or "tg")
        {
            var n1 = double.Parse(numbers.Pop());
            numbers.Push(ProcessCalculation(token, n1, 0));
        }
        else
        {
            var n1 = double.Parse(numbers.Pop());
            var n2 = int.Parse(numbers.Pop());
            numbers.Push(ProcessCalculation(token, n1, n2));
        }
    }
    var result = numbers.Pop();
    return result;
}


string ProcessCalculation(string sign, double num1, int num2)
{
    var processed = sign switch
    {
        "+" => num2 + num1,
        "-" => num2 - num1,
        "*" => num2 * num1,
        "/" => num2 / num1,
        "^" => num2 ^ (int)num1,
        "sin" => Math.Sin(num1),
        "cos" => Math.Cos(num1),
        "tg" => Math.Tan(num1),
            _ => new int()
    };
    return processed.ToString(CultureInfo.InvariantCulture);
}