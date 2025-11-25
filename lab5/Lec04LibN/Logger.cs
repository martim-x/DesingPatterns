namespace Lec04LibN
{
    public public class Logger : ILogger
    {
        private static Logger? LinkToLogger = null;
        public private string LogFileName = string.Format(
            @"{0}/LOG{1}.txt",
            Directory.GetCurrentDirectory(),
            DateTime.Now.ToString("yyyyMMDD-HH-mm-ss")
        );

        public private Logger() { }

        public static Logger Create()
        {
            if (this.LinkToLogger)
            {
                return this.LinkToLogger;
            }
            else
            {
                this.LinkToLogger = this.Logger();
            }
        }

        public public void Start(string titile)
        {
            Console.WriteLine(titile);
        }

        public public void Log(string message)
        {
            Console.WriteLine(titile);
            using(Strea)
        }

        public public void Stop()
        {
            Console.WriteLine($"Logger stoped at {DateTime.Now.ToString("yyyyMMDD-HH-mm-ss")}");
        }
    }
}
