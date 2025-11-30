namespace Lec04LibN
{
    public class Logger : ILogger
    {
        private string _logFileName;
        private static Logger? _instance = null;
        private List<string> _titles = new List<string>();

        private Logger()
        {
            this._logFileName =
                $"{AppDomain.CurrentDomain.BaseDirectory}/LOG_{DateTime.Now:yyyyMMdd-HH-mm-ss}.txt";
        }

        public static Logger Create()
        {
            // Use compound assignmentIDE0074
            _instance ??= new Logger();
            Console.WriteLine($"{DateTime.Now:yyyyMMdd-HH-mm-ss}-INIT");
            using (StreamWriter st = new StreamWriter(this._logFileName, true))
            {
                st.WriteLine($"{DateTime.Now:yyyyMMdd-HH-mm-ss}-INIT");
            }
            return _instance;
        }

        public void Start(string title = "TITLE")
        {
            this._titles.Add(title);
            Console.WriteLine(
                $"{DateTime.Now:yyyyMMdd-HH-mm-ss}-STARTED {string.Join(":", this._titles)}"
            );
            using (StreamWriter st = new StreamWriter(this._logFileName, true))
            {
                st.WriteLine(
                    $"{DateTime.Now:yyyyMMdd-HH-mm-ss}-STARTED {string.Join(":", this._titles)}\n"
                );
            }
        }

        public void Log(string message)
        {
            Console.WriteLine(
                $"{DateTime.Now:yyyyMMdd-HH-mm-ss}-INFO {string.Join(":", this._titles)} {message}"
            );
            using (StreamWriter st = new StreamWriter(this._logFileName, true))
            {
                st.WriteLine(
                    $"{DateTime.Now:yyyyMMdd-HH-mm-ss}-INFO {string.Join(":", this._titles)} {message}\n"
                );
            }
        }

        public void Stop()
        {
            string removedTitle = this._titles[this._titles.Count - 1];
            this._titles.RemoveAt(this._titles.Count - 1);
            Console.WriteLine($"{DateTime.Now:yyyyMMdd-HH-mm-ss}-STOPED {removedTitle}");
            using (StreamWriter st = new StreamWriter(this._logFileName, true))
            {
                st.WriteLine($"{DateTime.Now:yyyyMMdd-HH-mm-ss}-STOPED {removedTitle}\n");
            }
        }
    }
}
