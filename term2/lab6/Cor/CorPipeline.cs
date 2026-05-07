namespace CorLib;

public sealed class CorPipeline<TContext> : ICorPipeline<TContext>
    where TContext : ICorContext
{
    public ICorHandler<TContext>? First { get; private set; }

    private ICorHandler<TContext>? _last;

    public ICorPipeline<TContext> Add(ICorHandler<TContext> handler)
    {
        if (First is null)
        {
            // первая вставка
            First = handler;
            _last = handler;
        }
        else
        {
            // цепляем к последнему
            _last!.Next = handler;
            _last = handler;
        }

        return this;
    }

    public void ExecuteWithReverse(TContext context)
    {
        if (First is null)
        {
            Console.WriteLine("[PIPELINE] Цепочка пустая");
            return;
        }

        var stack = new Stack<ICorHandler<TContext>>();

        try
        {
            // ПРЯМОЙ ПРОХОД
            var current = First;

            while (current is not null)
            {
                current.Handle(context);

                // если есть Next, кладём в стек
                if (current.Next is not null)
                {
                    stack.Push(current);
                }

                current = current.Next;
            }

            Console.WriteLine(
                $"[PIPELINE SUCCESS]: X = {context.X}, Y = {context.Y}, Z = {context.Z}"
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[PIPELINE ERROR]: {ex.Message}");
        }

        // ОБРАТНЫЙ ПРОХОД (и при успехе, и при ошибке — при желании можешь
        // обернуть это в `if (была_ошибка)`)

        Console.WriteLine("[PIPELINE REVERSE]");

        while (stack.Count > 0)
        {
            var handler = stack.Pop();
            handler.Handle(context);
        }

        Console.WriteLine(
            $"[PIPELINE AFTER REVERSE]: X = {context.X}, Y = {context.Y}, Z = {context.Z}"
        );
    }
}
