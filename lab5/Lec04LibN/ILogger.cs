namespace Lec04LibN
{
    interface ILogger
    {
        void Start(string title);
        void Log(string message);
        void Stop();

        Logger Create();
    }
}
