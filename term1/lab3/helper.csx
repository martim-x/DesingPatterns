public class Calculator
{
    private int _x,
        _y;
    private string _operation;

    public Calculator()
        : this(0, 0)
    {
        Console.WriteLine("Конструктор 1");
    }

    public Calculator(int x, int y)
        : this(x, y, "add")
    {
        Console.WriteLine("Конструктор 2");
    }

    public Calculator(int x, int y, string operation)
    {
        _x = x;
        _y = y;
        _operation = operation;
        Console.WriteLine("Конструктор 3");
    }

    public Calculator(string operation)
        : this(10, 20, operation) // Вызывает конструктор 3
    {
        Console.WriteLine("Конструктор 4");
    }
}

var calc = new Calculator();
