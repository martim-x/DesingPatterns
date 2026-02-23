namespace Services;

public partial class Math
{
    // Результат цепочки вычислений (для instance-методов)
    public int result { get; private set; } = 0;

    public Math() { }

    public Math(int result)
    {
        this.result = result;
    }

    // Статический метод — Builder.Create() без this
    public static Builder getBuilder() => Builder.Create();

    // ── Двухаргументные статические вычисления
    public int Sum(int x, int y) => x + y;

    public int Diff(int x, int y) => x - y;

    public int Mul(int x, int y) => x * y;

    public int Div(int x, int y) => x / y;

    public int Mod(int x, int y) => x % y;

    // ── Цепочные методы (мутируют result и возвращают this)
    public Math Sum(int y)
    {
        result += y;
        return this;
    }

    public Math Diff(int y)
    {
        result -= y;
        return this;
    }

    public Math Mul(int y)
    {
        result *= y;
        return this;
    }

    public Math Div(int y)
    {
        result /= y;
        return this;
    }

    public Math Mod(int y)
    {
        result %= y;
        return this;
    }
}
