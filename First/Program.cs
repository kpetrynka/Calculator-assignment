using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Input the calculation
        Console.WriteLine("Enter expression:");
        string expression = Console.ReadLine();

        // Do the tokenization
        List<string> tokens = new List<string>();
        string number = string.Empty;
        foreach (char i in expression)
        {
            var success = int.TryParse(i.ToString(), out int result);
            if (success)
            {
                number += result.ToString();
            }
            else
            {
                if (!string.IsNullOrEmpty(number))
                {
                    tokens.Add(number);
                    number = string.Empty;
                }
                if (i != ' ')
                {
                    tokens.Add(i.ToString());
                }
            }
        }
        if (!string.IsNullOrEmpty(number))
        {
            tokens.Add(number);
        }

        // Write the Polish notation of the expression
        Queue<string> notation = new Queue<string>();
        Stack<string> operators = new Stack<string>();
        foreach (string token in tokens)
        {
            if (int.TryParse(token, out int num))
            {
                notation.Enqueue(num.ToString());
            }
            else if (token == "(")
            {
                operators.Push(token);
            }
            else if (token == ")")
            {
                while (operators.Count > 0 && operators.Peek() != "(")
                {
                    notation.Enqueue(operators.Pop());
                }
                if (operators.Count == 0)
                {
                    throw new Exception("Unmatched parentheses");
                }
                operators.Pop();
            }
            else // token is an operator
            {
                while (operators.Count > 0 && priority[token] <= priority[operators.Peek()])
                {
                    notation.Enqueue(operators.Pop());
                }
                operators.Push(token);
