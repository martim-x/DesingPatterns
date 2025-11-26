namespace Lec04LibN
{
    public class Logger : ILogger
    {
        private static string logFileName =
            $"{AppDomain.CurrentDomain.BaseDirectory}/LOG_{DateTime.Now:yyyyMMdd-HH-mm-ss}.txt";
        private static Logger? _instance = null;
        private static List<string> _titles = new List<string>();

        private Logger() { }

        public static Logger Create()
        {
            // Use compound assignmentIDE0074
            _instance ??= new Logger();
            Console.WriteLine($"{DateTime.Now:yyyyMMdd-HH-mm-ss}-INIT");
            using (StreamWriter st = new StreamWriter(logFileName, true))
            {
                st.WriteLine($"{DateTime.Now:yyyyMMdd-HH-mm-ss}-INIT");
            }
            return _instance;
        }

        public void Start(string title = "TITLE")
        {
            _titles.Add(title);
            Console.WriteLine(
                $"{DateTime.Now:yyyyMMdd-HH-mm-ss}-STARTED {string.Join(":", _titles)}"
            );
            using (StreamWriter st = new StreamWriter(logFileName, true))
            {
                st.WriteLine(
                    $"{DateTime.Now:yyyyMMdd-HH-mm-ss}-STARTED {string.Join(":", _titles)}\n"
                );
            }
        }

        public void Log(string message)
        {
            Console.WriteLine(
                $"{DateTime.Now:yyyyMMdd-HH-mm-ss}-INFO {string.Join(":", _titles)} {message}"
            );
            using (StreamWriter st = new StreamWriter(logFileName, true))
            {
                st.WriteLine(
                    $"{DateTime.Now:yyyyMMdd-HH-mm-ss}-INFO {string.Join(":", _titles)} {message}\n"
                );
            }
        }

        public void Stop()
        {
            string removedTitle = _titles[_titles.Count - 1];
            _titles.RemoveAt(_titles.Count - 1);
            Console.WriteLine($"{DateTime.Now:yyyyMMdd-HH-mm-ss}-STOPED {removedTitle}");
            using (StreamWriter st = new StreamWriter(logFileName, true))
            {
                st.WriteLine($"{DateTime.Now:yyyyMMdd-HH-mm-ss}-STOPED {removedTitle}\n");
            }
        }
    }
}
