namespace Physical_Game_Log_Explorer;

public static class Program
{
    #region Variables
    static string logFilePath = Environment.CurrentDirectory + "\\Logfile.txt";
    static string savedLogsFilePath = Environment.CurrentDirectory + "LogHistory.txt";
    static JSONStorageClass jsonStorage;
    static GameStats stats;

    #endregion

    #region Functions
    static void Main(string[] args)
    {
        Menu.Instance().Run();
    }

    public static void ImportLogFromFile()
    {
        Console.Clear();
        Console.WriteLine(AppStrings.Instance().Importing);
        ProcessLogFile();
        Console.Clear();
        Console.WriteLine(AppStrings.Instance().ImportFinished);
        Console.ReadKey();
    }
    static void ProcessLogFile()
    {
        jsonStorage = new();
        int currentGame = 0;
        List<int> times = new();
        using ( StreamReader reader = new(logFilePath))
        {
            string logLine;
            do
            {
                logLine = reader.ReadLine();
                if (logLine.Contains("[START]"))
                {
                    stats = new();
                    currentGame++;
                    jsonStorage.Date = logLine.Substring(logLine.IndexOf(':')+1, 10);
                    stats.GameNumber = currentGame;
                    stats.StartTime = logLine.Substring(logLine.IndexOf(':')+12, 8);
                }
                else if (logLine.Contains("[TIME]"))
                {
                    times.Add(int.Parse(logLine.Substring(logLine.IndexOf(':')+1)));
                }
                else if(logLine.Contains("WrongButton"))
                {
                    stats.FailureType = "Hitting the wrong button.";
                }
                else if(logLine.Contains("TimeOut"))
                {
                    stats.FailureType = "Not hitting in time.";
                }
                else if(logLine.Contains("[END]"))
                {
                    stats.AverageTimeToHit = GetAverageTime(times);
                    stats.ButtonsHitAmount = times.Count;
                    jsonStorage.gameStats.Add(stats);
                }
            } while(logLine != null);
        }
        int highestScore = 0;
        double averageTimeTotal = 0;
        foreach(GameStats stats in jsonStorage.gameStats)
        {
            if(highestScore < stats.ButtonsHitAmount)
            {
                highestScore = stats.ButtonsHitAmount;
            }
            averageTimeTotal += stats.AverageTimeToHit;
        }
        jsonStorage.AverageTimeAllGames = averageTimeTotal / jsonStorage.gameStats.Count;
        jsonStorage.TopScore = highestScore;
        Root.Days.Add(jsonStorage);
    }
    static double GetAverageTime(List<int> times)
    {
        double totalTime = 0;
        foreach(double time in times)
        {
            totalTime += time;
        }
        return totalTime / times.Count;
    }

    #endregion
}
