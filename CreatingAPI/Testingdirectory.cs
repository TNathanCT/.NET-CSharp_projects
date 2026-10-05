namespace InheritanceApp
{


    class Program
    {
        static void Main(string[] args)
        {
            string directorypath = @"C:\Logs";
            string filepath = System.IO.Path.Combine(directorypath, "log.txt");
            string message = "This is my message";

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(directorypath);
            }
             File.AppendAllText(filepath, message + "\n");
            Console.ReadKey();
        }
    }


}
