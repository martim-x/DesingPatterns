namespace Services;

internal class Decorator : IMathWrapper
{
    protected int a = 0;

    public Decorator(int a)
    {
        this.a = a;
    }

    public virtual int Wrapper(int value)
    {
        return value; // базовое поведение
    }
}

internal class Result : Decorator
{
    public Result()
        : base(0) { }

    // Просто возвращает value — начальная точка цепочки
    public override int Wrapper(int value) => value;
}

internal class Sum : Decorator
{
    IMathWrapper decorator;

    public Sum(int a, IMathWrapper decorator)
        : base(a)
    {
        this.decorator = decorator;
    }

    // Передаём value дальше по цепочке, потом прибавляем своё a
    public override int Wrapper(int value) => this.decorator.Wrapper(value) + this.a;
}

internal class Diff : Decorator
{
    IMathWrapper decorator;

    public Diff(int a, IMathWrapper decorator)
        : base(a)
    {
        this.decorator = decorator;
    }

    public override int Wrapper(int value) => this.decorator.Wrapper(value) - this.a;
}

internal class Mul : Decorator
{
    IMathWrapper decorator;

    public Mul(int a, IMathWrapper decorator)
        : base(a)
    {
        this.decorator = decorator;
    }

    public override int Wrapper(int value) => this.decorator.Wrapper(value) * this.a;
}

internal class Div : Decorator
{
    IMathWrapper decorator;

    public Div(int a, IMathWrapper decorator)
        : base(a)
    {
        this.decorator = decorator;
    }

    public override int Wrapper(int value) => this.decorator.Wrapper(value) / this.a;
}

internal class Mod : Decorator
{
    IMathWrapper decorator;

    public Mod(int a, IMathWrapper decorator)
        : base(a)
    {
        this.decorator = decorator;
    }

    public override int Wrapper(int value) => this.decorator.Wrapper(value) % this.a;
}
