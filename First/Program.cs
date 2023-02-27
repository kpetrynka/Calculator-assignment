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
    var output = new Queue();
    foreach (char i in inputted)
    {
        if (char.IsDigit(i))
        {
            n += i.ToString();
        }
        else if (char.IsWhiteSpace(i))
        {
            output.Add(n);
            n = "";
        }
        else
        {
            output.Add(n);
            output.Add(i.ToString());
            n = "";
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
        ["-"] = 2,
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
        else
        {
            if (operatorStack.IsEmpty() 
                || operatorStack.Contains("(") 
                || priority[token] <= priority[operatorStack.Peek()])
            {
                operatorStack.Push(token);
            }
            if (token == "(")
            {
                operatorStack.Push(token);
            }
            if (token == ")")
            { 
                while (operatorStack.Pop() != "(")
                {
                    output.Add(operatorStack.Pop());
                }
                operatorStack.Pop();
            }
            if (priority[token] > priority[operatorStack.Peek()])
            {
                  output.Add(operatorStack.Pop());          
            }
        }
    }

    operatorStack.Pop();

    while (!operatorStack.IsEmpty())
    {
        var sing = operatorStack.Pop();
        output.Add(sing);
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
        else
        {
            var num1 = int.Parse(numbers.Pop());
            var num2 = int.Parse(numbers.Pop());
            numbers.Push(ProcessCalculation(token, num1, num2));
        }
    }
    var result = numbers.Pop();
    return result;
}


string ProcessCalculation(string sign, int num1, int num2)
{
    var processed = sign switch
    {
        "+" => num1 + num2,
        "-" => num1 - num2,
        "*" => num1 * num2,
        "/" => num1 / num2,
        "^" => num1 ^ num2,
        _ => new int()
    };
    return processed.ToString();
}