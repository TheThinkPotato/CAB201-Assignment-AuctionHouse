using System;
using CAB201;

namespace AuctionHouse
{
    public abstract class Menu
    {
        protected int selection;
        protected string[] menuOptions;
        protected string menuTitle;

        public Menu()
        // Construct with a default menu option.
        {
            menuOptions = new string[] {"Exit"};
        }

        public void Display()
        // Disaplays the Menu with the List from MenuOptions List
        {
            selection = 0;
            while (selection != menuOptions.Length -1){
                Console.Clear();
                selection = UserInterface.GetOption($"{menuTitle}", menuOptions);
                SelectOptions(selection);
            }         
        }
        public abstract void SelectOptions(int select);
        // Menu options presented to user.

        public void DisplaySubMenuTitle(string menuName)
        // makes underlinetitle for submenus
        {
            Console.Clear();
            Console.WriteLine(DisplayMenuTitle(menuName));
        }
        public string DisplayMenuTitle(string menuName)
        // Makes the underline title for menus
        {
            string underline = string.Empty;
            for (int i = 0; i < menuName.Length; i++){
                underline += "*";
            }
            return "\t" + menuName + "\n\t" + underline + "\n";            
        }

        public void Message(string input)
        // Displays message sent to it followed by a press enter to continue message.
        {
            UserInterface.Message(input);
            UserInterface.Message("Press 'Enter' to continue.");
            Console.ReadLine();
        }

        public void Message() { this.Message(""); }
        //overload  - displays just the press enter message.

        public void DisplayBar(int barLength, char barCharacter)
        // draws a bar of characters across the screen.
        {
            string bar = string.Empty;
            for (int i = 0; i < barLength; i++){
                bar += barCharacter;
            }
            Console.WriteLine(bar);
        }

        public string GetStringInput(string textPrompt)
        // Gets String Input Via User Interface Class
        {
            return UserInterface.GetInput(textPrompt);
        }

        public string GetPasswordInput(string textPrompt)
        // Gets Password Input Via User Interface Class
        {
            return UserInterface.GetPassword(textPrompt);
        }

        public int GetIntInput(string textPrompt)
        // Gets Int Input Via User Interface Class
        {
            return UserInterface.GetInt(textPrompt);
        }

        public double GetDoubleInput(string textPrompt)
        // Gets Double Input Via User Interface Class
        {
            return UserInterface.GetDouble(textPrompt);
        }
    }
}