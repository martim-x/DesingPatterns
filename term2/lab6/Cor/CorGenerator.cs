namespace CorLib;

public sealed class CorGenerator<TContext> : ICorGenerator<TContext>
    where TContext : ICorContext
{
    public ICorHandler<TContext>? First { get; set; }

    public void Start(TContext context)
    {
        First?.Handle(context);
        Console.WriteLine($"[START]: X = {context.X}, Y = {context.Y}, Z = {context.Z}");
    }
}
