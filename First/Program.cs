using System.Diagnostics;
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
    Console.WriteLine(transformed);
    Console.WriteLine("Try again? +/-");
    string answer = Console.ReadLine() ?? throw new InvalidOperationException();
    if (answer != "+")
    {
        stage = false;
    }
}

//do the tokens;
Queue ToToken(string inputted)
{ ;
    var n = "";
    var output = new Queue();
    foreach (char i in inputted)
    {
        if (i != null)
        {
            if (int.TryParse(i, out _)
            { 
                n += i.ToString();
            }
            else
            {
                output.Add(n);
                output.Add(i.ToString());
            }
        }
        return output;
}

// write the polish notation of the expression
Dictionary<string, int> priority = new Dictionary<string, int>() //we will need it for polish notation
{
    ["+"] = 1,
    ["-"] = 2,
    ["*"] = 2,
    ["/"] = 2,
    ["^"] = 2,
};

Queue PostFix(Queue tokens)
{
    string[] operators = { "+", "-", "*", "/","^"};
    var operatorStack = new Stack();
    var output = new Queue();
    while (tokens.Out() != null)
    {
        var token = tokens.Out();
        var checkIfNumber = double.TryParse(token, NumberStyles.Any, CultureInfo.InvariantCulture, out _);
        var checkIfOperator = operators.Contains(token);
        if (checkIfNumber)
        {
            output.Add(token);
        }
        if (checkIfOperator)
        {
            if (operatorStack == null && operatorStack.Contains("("))
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
                operatorStack.Push(token); 
            }

            if (priority[token] >= priority[operatorStack.Peek()]) continue;
            while (priority[token] > priority[operatorStack.Peek()])
            {
                operatorStack.Push(token); 
                    
            }
            operatorStack.Pop();
            //Print the operand as they arrive.
//   If the stack is empty or contains a left parenthesis on top, push the incoming operator on to the stack.
// If the incoming symbol is '(', push it on to the stack.
//   If the incoming symbol is ')', pop the stack and print the operators until the left parenthesis is found.
//    If the incoming symbol has higher precedence than the top of the stack, push it on the stack.
//    If the incoming symbol has lower precedence than the top of the stack, pop and print the top of the stack. Then test the incoming operator against the new top of the stack.
//    If the incoming operator has the same precedence with the top of the stack then use the associativity rules. If the associativity is from left to right then pop and print the top of the stack then push the incoming operator. If the associativity is from right to left then push the incoming operator.
            // At the end of the expression, pop and print all the operators of the stack.  
            
        }
    }

    while (!operatorStack.IsEmpty())
    {
        output.Enqueue(operatorStack.Pull());
    }

    return output;
}


string CalculationPerformed(Queue postFixed)
{
    var numbers = new Stack();
    while (postFixed.Out() != null)
    {
        var token = postFixed.Out();
        if (int.TryParse(token, out int num))
        {
            numbers.Push(num.ToString());
        }
        else
        {
            var num1 = int.Parse(numbers.Pull());
            var num2 = int.Parse(numbers.Pull());
            numbers.Push(ProcessCalculation(token, num1, num2));
        }
    }
    var result = numbers.Pull();
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