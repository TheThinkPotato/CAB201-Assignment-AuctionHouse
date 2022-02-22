using System;
using System.Collections.Generic;

namespace AuctionHouse
{
    class Program
    {
        static private List<Client> clientList = new List<Client>();        
        static private Client activeClient = null;
        static private Menu myMenu = new MainMenu();

        static public List<Client> ClientList
        {
            get
            {
                return clientList;
            }
            set
            {
                clientList = value;
            }
        }

        static public Client ActiveClient
        {
            get
            {
                return activeClient;
            }
            set
            {
                activeClient = value;
            }
        }
        static public Menu MyMenu
        { get
            {
                return myMenu;
            }
            private set
            {
                myMenu = value;
            }
        }
        static void Main(string[] args)
        {
            DisplaySplashScreen();            
            myMenu.Display();
        }
        
        static private void DisplaySplashScreen()
            //Displays Splash Screen with Details.
        {
            myMenu.DisplayBar(70, '=');
            Console.WriteLine("\t\tAuction House Assignmnet For CAB201\n\t\tBy Daniel Lopez\t QUT ID No: 10956611");
            myMenu.DisplayBar(70, '=');
            myMenu.Message();
        }
    }
}
