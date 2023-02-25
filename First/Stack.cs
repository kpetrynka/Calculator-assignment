using System.Reflection.Metadata.Ecma335;

namespace First;


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

    public string Pop()
    {
        if (_pointer == 0)
        {
            return null;
        }

        var value = _array[_pointer];
        _pointer--;
        return value;
    }

    public string Peek()
    {
        if (_pointer == 0)
        {
            return null;
        }

        var value = _array[_pointer - 1];
        return value;
    }

    public bool Contains(string value)
    {
        var exists = 0;
        foreach (var i in _array)
        {
            if (i == value)
            {
                exists += 1;
            }
        }
        return exists > 0;
    }
}