namespace CorLib;

public sealed class Context : ICorContext
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public int Z { get; private set; }

    public Context(int x = 0, int y = 0, int z = 0)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public int Sum() => Z += X + Y;

    public int Sub() => Z += X - Y;

    public int Mul() => Z += X * Y;
}
