namespace CorLib;

public sealed class Handler<TContext> : ICorHandler<TContext>
    where TContext : ICorContext
{
    public ICorHandler<TContext>? Next { get; set; }

    private readonly Action<TContext> _action;

    public Handler(Action<TContext> action)
    {
        _action = action ?? throw new ArgumentNullException(nameof(action));
    }

    public void Handle(TContext context)
    {
        _action(context);
    }
}
