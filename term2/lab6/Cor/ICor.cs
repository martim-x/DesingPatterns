namespace CorLib;

public interface ICorContext
{
    int X { get; }
    int Y { get; }
    int Z { get; }

    int Sum();
    int Sub();
    int Mul();
}

public interface ICorGenerator<TContext>
    where TContext : ICorContext
{
    ICorHandler<TContext>? First { get; set; }
    void Start(TContext context);
}

public interface ICorHandler<TContext>
    where TContext : ICorContext
{
    ICorHandler<TContext>? Next { get; set; }
    void Handle(TContext context);
}

public interface ICorPipeline<TContext>
    where TContext : ICorContext
{
    ICorPipeline<TContext> Add(ICorHandler<TContext> handler);
    ICorHandler<TContext>? First { get; }
    void ExecuteWithReverse(TContext context);
}
