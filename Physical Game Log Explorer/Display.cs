namespace Physical_Game_Log_Explorer;

public class Display
{
    int DaysAmount { get; set; }
    string[] Dates { get; set; }
    JSONStorageClass[] DayStorage {get; set;}
    GameStats[] GamesStorage {get; set;}
    int gamesAmount = 0;
    int position = 0;
    int detailsPosition = 0;
    bool isDirty = true;
    bool details = false;
    bool isRunning = true;
    
    public Display()
    {
        DaysAmount = Root.Days.Count;
        Dates = new string[Root.Days.Count];
        DayStorage = new JSONStorageClass[Root.Days.Count];
        int counter = 0;
        foreach(JSONStorageClass jsonStorage in Root.Days)
        {
            Dates[counter] = jsonStorage.Date;
            DayStorage[counter] = jsonStorage;
            counter++;
        }
        RunDisplay();
    }

    void RunDisplay()
    {
        while(isRunning)
        {
            Draw();
            Input();
        }

    }

    void Draw()
    {
        if(isDirty)
        {
            Console.Clear();
            Console.WriteLine("Date: "+ Dates[position]);
            if(details)
            {
                Console.WriteLine("< Previous | ^ Back ^ | Next >");
                Console.WriteLine("Played at: "+ GamesStorage[detailsPosition].StartTime);
                Console.WriteLine("Game #: "+ GamesStorage[detailsPosition].GameNumber);
                Console.WriteLine("Buttons hit: "+ GamesStorage[detailsPosition].ButtonsHitAmount);
                Console.WriteLine("Average Speed (Time to click): " + GamesStorage[detailsPosition].AverageTimeToHit);
                Console.WriteLine("Lost due to: "+ GamesStorage[detailsPosition].FailureType);
                   
            }
            else
            {
                Console.WriteLine("< Previous | v Details v | Next >");
                Console.WriteLine("Overview:");
                Console.WriteLine("Games Played: "+ DayStorage[position].gameStats.Count);
                Console.WriteLine("High Score: "+ DayStorage[position].TopScore);
                Console.WriteLine("Average Speed (All Games): "+ DayStorage[position].AverageTimeAllGames);
            }
        }
    }
    void Input()
    {
        if(Console.KeyAvailable)
        {
            ConsoleKeyInfo info = Console.ReadKey();
            if(info.Key == ConsoleKey.DownArrow && details == false)
            {
                UpdateDetails();
                details = true;
                isDirty = true;
            }
            else if(info.Key == ConsoleKey.RightArrow)
            {
                if(details)
                {
                    if(detailsPosition != gamesAmount-1)
                    {
                        detailsPosition++;
                    }
                }
                else
                {
                    if(position != DaysAmount-1)
                    {
                        position++;
                    }
                }
                isDirty = true;
            }
            else if(info.Key == ConsoleKey.LeftArrow)
            {
                if(details)
                {
                    if(detailsPosition != 0)
                    {
                        detailsPosition--;
                    }
                }
                else
                {
                    if(position != 0)
                    {
                        position--;
                    }
                }
                isDirty = true;
            }
            else if(info.Key == ConsoleKey.UpArrow && details == true)
            {
                details = false;
                isDirty = true;
            }
            else if(info.Key == ConsoleKey.Escape)
            {
                isRunning = false;
            }
        }
    }
    void UpdateDetails()
    {
        gamesAmount = DayStorage[position].gameStats.Count;
        GamesStorage = new GameStats[gamesAmount];
        int counter = 0;
        foreach(GameStats stats in DayStorage[position].gameStats)
        {
            GamesStorage[counter] = stats;
            counter++;
        }
    }
}
