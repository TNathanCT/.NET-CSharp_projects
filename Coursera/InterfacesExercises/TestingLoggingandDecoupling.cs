namespace InheritanceApp
{
public interface ILogger
    {
        public void Log(string message);
    }



    public class FileLogger : ILogger
    {
        public void Log(string message)
        {
            string directorypath = @"C:\Logs";
            string filepath = System.IO.Path.Combine(directorypath, "log.txt");

            if (!Directory.Exists(directorypath))
            {
                Directory.CreateDirectory(directorypath);
            }
            File.AppendAllText(filepath, message + "\n");

        }
    }

    public class DataBaseLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"Logging to database: {message}");
        }
    }


    public class Application
    {
        private readonly ILogger loggerinterface;


        public Application(ILogger _logger)
        {
            loggerinterface = _logger;
        }

        public void DoWork()
        {
            loggerinterface.Log("Do Work");
            loggerinterface.Log("Work done!");
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            ILogger filelogger = new FileLogger();
            Application app = new Application(filelogger);
            app.DoWork();


            ILogger dblogger = new DataBaseLogger();
            app = new Application(dblogger);
            app.DoWork();

            Console.ReadKey();
        }
    }


}
