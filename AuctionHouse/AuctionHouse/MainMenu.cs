using System;


namespace AuctionHouse
{
    public class MainMenu : Menu
    {
        public MainMenu()
        {
            menuOptions = new string[] { "Regester New Client","Log In","Log Out","Exit"};
            menuTitle = DisplayMenuTitle("Main Menu");
        }

        public override void SelectOptions(int select)
        // Main menu selection options.
        {
            bool loggedIn = Program.ActiveClient != null;
            switch (select)
            {
                case 0:
                    if (loggedIn)
                        Message("\nPlease Log Out");
                    else
                        Regester();
                    break;

                case 1:
                    // If logged in bypass loggin prompt
                    if (loggedIn)
                    {
                        Program.ActiveClient.MyAuctionHouse.MyAuctionMenu.Display();
                    }
                    else
                    {
                        if (LogIn()){
                            UpdateMenuOptions();
                            Program.ActiveClient.MyAuctionHouse.MyAuctionMenu.Display();
                        }
                    }
                    break;

                case 2:
                    LogOut();
                    UpdateMenuOptions();
                    break;

                default:
                    break;
            }
        }

        public void UpdateMenuOptions()
        // Updates Main menu items when logged in
        {
            if (Program.ActiveClient == null){
                menuOptions[1] = "Log In";
            }
            else{
                menuOptions[1] = "Auction Menu";
            }
        }

        private void Regester()
        // Regesters a new client to the auctionhouse
        {
            bool userExists = false;
            
            DisplaySubMenuTitle("Regester New Client");
            string name = GetStringInput("Press 'Enter' to go back.\n\nPlease enter your name");
            if (name != string.Empty){
                string email = GetStringInput("\nPlease enter your email address");

                //Check if user exists already
                foreach (var client in Program.ClientList){
                    
                    if (client.Email == email){
                        userExists = true;
                        break;
                    }
                }

                if (userExists){
                    Program.MyMenu.Message("\nUser already exists");
                }
                else{
                    string address = GetStringInput("\nPlease enter your home address");
                    string password = GetPasswordInput("\nPlease enter your password");
                    Program.ClientList.Add(new Client(name, email, address, password));
                    Message($"\nWelcome to the Auction House {name}.");
                }
            }
        }

        public bool LogIn()
        // Client log in
        {
            DisplaySubMenuTitle("Log In");
            string inputClientEmail = GetStringInput("Please enter you email address");
            string inputPassword = GetPasswordInput("Please enter your password");

            // check if users email is already in list.
            for (int i = 0; i < Program.ClientList.Count; i++){

                if (Program.ClientList[i].Email == inputClientEmail)
                {
                    // Check if passsword is correct and return boolean value
                    if (Program.ClientList[i].PasswordCheck(inputPassword)){
                        Program.ActiveClient = Program.ClientList[i];
                        Message($"Welcome back {Program.ActiveClient.Name}");
                        return true;
                    }
                }
            }            
            Message("Invalid username or password.");
            return false;
        }
        public void LogOut()
        // Client log out and also check log in status.
        {
            DisplaySubMenuTitle("Log Out");

            if (Program.ActiveClient != null){
                Message($"Good bye {Program.ActiveClient.Name}.");
                Program.ActiveClient = null;
            }
            else
                Message("You are not logged in.");
        }
    }
}