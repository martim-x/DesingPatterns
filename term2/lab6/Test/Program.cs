using System;
using CorLib;

public static class Program
{
    public static void Main(string[] args)
    {
        Empty_Context();
        Console.WriteLine();

        Success();
        Console.WriteLine();

        Error();
    }

    private static void Empty_Context()
    {
        Console.WriteLine("=== Empty_Context ===");
        var pipeline = new CorPipeline<ICorContext>();
        var context = new Context(3, 5, 7);

        pipeline.ExecuteWithReverse(context);

        Console.WriteLine($"[CONTEXT]: X={context.X}, Y={context.Y}, Z={context.Z}");
    }

    private static void Success()
    {
        Console.WriteLine("=== Success ===");

        var pipeline = new CorPipeline<ICorContext>()
            .Add(new Handler<ICorContext>(ctx => ctx.Sum()))
            .Add(new Handler<ICorContext>(ctx => ctx.Sub()))
            .Add(new Handler<ICorContext>(ctx => ctx.Mul()));

        var context = new Context(3, 5, 7);

        pipeline.ExecuteWithReverse(context);

        Console.WriteLine($"[CONTEXT]: X={context.X}, Y={context.Y}, Z={context.Z}");
        // ожидаем Z = 34
    }

    private static void Error()
    {
        Console.WriteLine("=== Error ===");

        var pipeline = new CorPipeline<ICorContext>()
            .Add(new Handler<ICorContext>(ctx => ctx.Sum()))
            .Add(new Handler<ICorContext>(ctx => ctx.Sub()))
            .Add(new Handler<ICorContext>(ctx => throw new InvalidOperationException("boom")))
            .Add(new Handler<ICorContext>(ctx => ctx.Mul()));

        var context = new Context(3, 5, 7);

        pipeline.ExecuteWithReverse(context);

        Console.WriteLine($"[CONTEXT]: X={context.X}, Y={context.Y}, Z={context.Z}");
        // по логике: Sum -> Sub -> ошибка -> reverse по стеку (Sub, Sum) => итог Z = 19
    }
}
