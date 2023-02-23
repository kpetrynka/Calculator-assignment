//Input the calculation

string expression= Console.ReadLine() ?? throw new InvalidOperationException();

//do the tokens;
string number = string.Empty;
foreach (char i in expression)
{
    var success = int.TryParse(i, out int result);
    ArrayList? tokens = null;
    switch (success)
    {
        case true:
        {
            number += result.ToString();
        }
            break;
        case false:
        {
            tokens.Add(number);
            number = string.Empty;
            if (i != null)
            {
                tokens.Add(i.ToString());
            }
            break;
        }
    } 
}

// write the polish notation of the expression
// so here is already created PolishNotation which is queue

// calculator
Queue notation = new Queue();
Stack numbers = new Stack();
int index = 0;
while (notation != null)
{
    string character = notation[index];
    if (int.TryParse(character, out int num))
    {
        numbers.Push(num.ToString());
    }
    else
    {
        //Calculation()
        int num1 = int.Parse(numbers.Pull());
        int num2 = int.Parse(numbers.Pull());
        numbers.Push(ProcessCalculation(notation[1], num1, num2));
    }

    index += 1;
}

int output = int.Parse(numbers[0]);

// Classes that we need
Dictionary<string, int> priority = new Dictionary<string, int>()
{
    { "+", 1 },
    { "-", 1 },
    { "*", 2 },
    { "/", 2 },
    { "^", 2 },
    { "(", 3 },
};

int ProcessCalculation(string oper, int num1, int num2)
{
    var result = new int();
    if (oper == "+")
    {
        result = num1 + num2;
    }
    if (oper == "-")
    {
        result = num1 - num2;
    }
    if (oper == "*")
    {
        result = num1 * num2;
    }
    if (oper == "/")
    {
        result = num1 / num2;
    }
    if (oper == "^")
    {
        result = num1 ^ num2;
    }
    return result;
}
public abstract class ArrayList
{
    private string?[] _array = new string?[10];

    private int _pointer = 0;
    public void Add(string element)
    {
        _array[_pointer] = element;
        _pointer += 1;

        if (_pointer == _array.Length)
        {
            string?[] extendedArray = new string[_array.Length * 2];
            for (var i = 0; i < _array.Length; i++)
            {
                extendedArray[i] = _array[i];
            }

            _array = extendedArray;
        }
    }
}

public class Queue
{
    private int[] _array = new int[10];

    private int _pointer = 0;

    public void Add(int element)
    {
        _array[_pointer] = element;
        _pointer += 1;

        if (_pointer == _array.Length)
        {
            var extendedArray = new int[_array.Length * 2];
            for (var i = 0; i < _array.Length; i++)
            {
                extendedArray[i] = _array[i];
            }

            _array = extendedArray;
        }
    }

    public void Pick(int smth)
    {
        _array[pointer];
    }

    public void Length()
    {
        int index = _pointer;
    }
}

public class Stack
{
    private const int Capacity = 50;
    private string[] _array = new string[Capacity];
    private int _pointer;

    
    public void Push(string value)
    {
        if (_pointer == _array.Length)
        {
            // this code is raising an exception about reaching stack limit
            throw new Exception("Stack overflowed");
        }

        _array[_pointer] = value;
        _pointer++;
    }

    public string Pull()
    {
        if (_pointer == 0)
        {
            //you can also raise an exception here, but we're simple returning nothing
            return null;
        }

        var value = _array[_pointer];
        _pointer--;
        return value;
    }
}