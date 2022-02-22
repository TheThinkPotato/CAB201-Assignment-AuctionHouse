using System;

namespace AuctionHouse
{
    public class AuctionMenu : Menu
    {
        public AuctionMenu()
        // Constructor setups the auction menu options and Menu Title.
        {
            menuOptions = new string[] { "Advertise a Product", "List All Advertised Products", "Search Auction House", "List Current Bids","Sell A Product", "Back"};
            menuTitle = DisplayMenuTitle("Auction Menu");
        }

        public override void SelectOptions(int select)
            // Auction Menus
        {
            switch (select)
            {
                case 0:
                    Program.ActiveClient.MyAuctionHouse.AdvertiseProduct();
                    break;

                case 1:
                    Program.ActiveClient.MyAuctionHouse.ListAdvertisedProducts();
                    break;

                case 2:
                    Program.ActiveClient.MyAuctionHouse.SearchProducts();
                    break;

                case 3:
                    Program.ActiveClient.MyAuctionHouse.ListBids();
                    break;

                case 4:
                    Program.ActiveClient.MyAuctionHouse.SellProduct();
                    break;

                default:
                    break;
            }
        }
    }
}