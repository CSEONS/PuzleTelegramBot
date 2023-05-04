namespace Bot.Data
{
    public class BotConfiguration
    {
        public static string ManagerListPath =>  "Data";
        public static long ManagerId = 1147571402;

        public const string TOKEN = "5861535570:AAFXgz6uFFbf0N3PmNDEfN71nmO-YyUA34E";
        public static string ManagerListFileName = "Manager.txt";
        internal static string ManagerListFullPath => $"{ManagerListPath}/{ManagerListFileName}";
    }
}
