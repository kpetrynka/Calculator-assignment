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

// Start the calculation

// Classes that we need

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
            //this also can be achieved via
            //Array.Resize(ref _array, _array.Length * 2);
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