namespace Physical_Game_Log_Explorer;
public static class Root
{
    public static List<JSONStorageClass> Days = new();
}
public class JSONStorageClass
{
    public string Date = "";
    public int TopScore = 0;
    public double AverageTimeAllGames = 0;
    public List<GameStats> gameStats = new(); 
}

public class GameStats
{
    public int GameNumber = 0;
    public string StartTime = "";
    public int ButtonsHitAmount = 0;
    public double AverageTimeToHit = 0;
    public string FailureType = "";
}
