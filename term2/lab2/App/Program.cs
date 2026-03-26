using Services;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("--------------------------------------------");

        Math calc1 = new Math();
        int x = 6,
            y = 4;
        Console.WriteLine("calc.{0}({1},{2}) = {3}", "Sum", x, y, calc1.Sum(x, y));
        Console.WriteLine("calc.{0}({1},{2}) = {3}", "Diff", x, y, calc1.Diff(x, y));
        Console.WriteLine("calc.{0}({1},{2}) = {3}", "Mul", x, y, calc1.Mul(x, y));
        Console.WriteLine("calc.{0}({1},{2}) = {3}", "Div", x, y, calc1.Div(x, y));
        Console.WriteLine("calc.{0}({1},{2}) = {3}", "Mod", x, y, calc1.Mod(x, y));

        Console.WriteLine("--------------------------------------------");

        Math calc2 = new Math(7);
        Console.WriteLine("calc2.result = {0}", calc2.result);
        Console.WriteLine(
            "calc2.Sum(5).Diff(4).Mul(2).Div(3).Mod(4).result = {0}",
            calc2.Sum(5).Diff(4).Mul(2).Div(3).Mod(4).result
        );

        Math calc3 = new Math(10);
        Console.WriteLine("calc3.result = {0}", calc3.result);
        Console.WriteLine(
            "calc3.Mul(2).Div(3).Sum(4).Mod(4).result = {0}",
            calc3.Mul(2).Div(3).Sum(4).Mod(4).result
        );

        Console.WriteLine("--------------------------------------------");

        var builder1 = Math.getBuilder();
        IMathWrapper calcresult1 = builder1.Sum(10).Sum(15).Mul(3).Build();
        var builder2 = Math.getBuilder();
        IMathWrapper calcresult2 = builder2.Mul(2).Mul(2).Sum(3).Build();
        IMathWrapper calcresult3 = builder2.Mul(4).Mul(4).Div(2).Mod(3).Build();

        int r1 = calcresult1.Wrapper(10); // ((10+10)+15)*3 = 105
        int r1x = calcresult1.Wrapper(-10); // ((-10+10)+15)*3 = 45
        int r2 = calcresult2.Wrapper(1); // ((1*2)*2) + 3 = 7
        int r2x = calcresult2.Wrapper(-1); // ((-1*2)*2) + 3 = -1
        int r3 = calcresult3.Wrapper(5); // (((5*4)*4)/2)%3 = 1

        Console.WriteLine("r1  = {0}", r1);
        Console.WriteLine("r1x = {0}", r1x);
        Console.WriteLine("r2  = {0}", r2);
        Console.WriteLine("r2x = {0}", r2x);
        Console.WriteLine("r3  = {0}", r3);
        Console.ReadKey();
    }
}
