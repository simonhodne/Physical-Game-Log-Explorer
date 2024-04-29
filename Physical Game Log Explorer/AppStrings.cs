namespace Physical_Game_Log_Explorer;

public class AppStrings
{
        private const int ENGLISH = 0;
        private static AppStrings _instance;
        public static AppStrings Instance()
        {
            if(_instance == null)
            {
                _instance = new();
            }
            return _instance;
        }

        public AppStrings()
        {
            TitleText = "Physical Game Log Explorer";
            MenuOptions = new string[4][];
            MenuOptions[0] = ["View Log History", "Import Log", "Exit"];
            Importing = "Importing...";
            ImportFinished = "Import Finished!";
        }

        public string TitleText {get; set;}
        public string[][] MenuOptions {get; set;}
        public string ImportFinished {get; set;}
        public string Importing {get; set;}
}
