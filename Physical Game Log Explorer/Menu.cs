namespace Physical_Game_Log_Explorer;

public class Menu
{
     private const string SELECTED_1 = "> \u001b[33m";
        private const string SELECTED_2 = "\u001b[0m <";
        private const string NOT_SELECTED = "  ";

        private string[][] fullMenu;
        private int menuState = 0;
        private int cursorPosition = 0;
        private string[] currentMenuItems;
        private int currentMenuLength = 0;
        public bool isRunning = true;
        private bool isDirty = true;
        
        private static Menu _instance;
        public static Menu Instance()
        {
            _instance = new(AppStrings.Instance().MenuOptions);
            return _instance;
        }
        public Menu(string[][] menuOptions)
        {
            currentMenuItems = GetMenuItems(menuOptions, menuState);
            currentMenuLength = GetMenuLength(currentMenuItems);
            fullMenu = menuOptions;
        }

        public void Run()
        { 
            while(isRunning)
            {
                Input(currentMenuLength, fullMenu);
                Draw(currentMenuItems, currentMenuLength);   
            }
        }

        private void Draw(string[] currentMenuItems, int menuLength)
        {
            if(isDirty)
            {
                Console.Clear();
                Console.WriteLine(AppStrings.Instance().TitleText);
                for (int i = 0; i < menuLength; i++)
                {
                    if (cursorPosition == i)
                    {
                        Console.WriteLine(SELECTED_1 + currentMenuItems[i] + SELECTED_2);
                    }
                    else
                    {
                        Console.WriteLine(NOT_SELECTED + currentMenuItems[i]);
                    }
                }
                isDirty = false;
            }
            
        }

        private void Input(int menuLength, string[][] fullMenu)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo info = Console.ReadKey();
                if(info.Key == ConsoleKey.UpArrow)
                {
                    if(cursorPosition == 0)
                    {
                        cursorPosition = menuLength-1;
                    }
                    else
                    {
                        cursorPosition--;
                    }
                }
                else if(info.Key == ConsoleKey.DownArrow)
                {
                    if(cursorPosition == menuLength-1)
                    {
                        cursorPosition = 0;
                    }
                    else
                    {
                        cursorPosition++;
                    }
                }
                else if(info.Key == ConsoleKey.Enter)
                {
                    Update();
                }
                else if(info.Key == ConsoleKey.Backspace)
                {
                    ChangeSubMenu(back: true);
                }

                isDirty = true;
            }
        }

        public void Update()
        {
            if (fullMenu[cursorPosition] != null && cursorPosition != 0)
            {
                ChangeSubMenu();
            }
            else if(cursorPosition == currentMenuItems.Length-1)
            {
                if (menuState == 0)
                {
                    isRunning = false;
                }
                else
                {
                    ChangeSubMenu(back: true);
                }
            }
            else if (menuState != 0)
            {
                
            }
            else
            {
                
                
            }
        }

        public void ChangeSubMenu(bool back = false)
        {
            if (back)
            {
                menuState = 0;
            }
            else
            {
                menuState = cursorPosition;
            }
            currentMenuItems = GetMenuItems(fullMenu, menuState);
            currentMenuLength = GetMenuLength(currentMenuItems);
        }
        
        private int GetMenuLength(string[] currentMenuItems)
        {
            return currentMenuItems.Length;
        }

        private string[] GetMenuItems(string[][] fullMenu, int menuState)
        {
            int length = fullMenu[menuState].Length;
            string[] menuItems = new string[length];
            for (int i = 0; i < length; i++)
            {
                menuItems[i] = fullMenu[menuState][i];
            }
            return menuItems;
        }
}
