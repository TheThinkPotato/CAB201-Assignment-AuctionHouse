using System;
using System.Collections.Generic;


namespace AuctionHouse
{
    public class AuctionHouse
    {
        private List<Product> advertisedProducts;
        private List<Product> soldProducts;
        private Menu myAuctionMenu;

        public List<Product> AdvertisedProducts
        {
            get
            {
                return advertisedProducts;
            }

            private set
            {
                advertisedProducts = value;
            }
        }
        public List<Product> SoldProducts
        {
            get
            {
                return soldProducts;
            }

            private set
            {
                soldProducts = value;
            }
        }
        public Menu MyAuctionMenu
        {
            get
            {
                return myAuctionMenu;
            }

            private set
            {
                myAuctionMenu = value;
            }
        }

        public AuctionHouse()
        // Constructor sets up clients auction house. Set up for thier products they are selling,
        // products the sell and there Auction menu.
        {
            AdvertisedProducts = new List<Product>();
            SoldProducts = new List<Product>();
            MyAuctionMenu = new AuctionMenu();
        }

        public void AdvertiseProduct()
        // Place item on the auction market
        {
            string menuName = "Advertise A Product";        
            myAuctionMenu.DisplaySubMenuTitle(menuName);

            string sellerEmail = Program.ActiveClient.Email;
            string inputType = myAuctionMenu.GetStringInput("Press 'Enter' to go back.\n\nType of Product");
            
            if (inputType != string.Empty){
                string inputName = myAuctionMenu.GetStringInput("\nName of Product");
                double inputInitialPrice = myAuctionMenu.GetDoubleInput("\nInitial Cost of Product in $");

                advertisedProducts.Add(new Product(sellerEmail, inputName, inputType, inputInitialPrice));
            }
        }

        public void ListAdvertisedProducts()
        // Show current advertised products
        {
            string menuName = "Advertised Products";
            myAuctionMenu.DisplaySubMenuTitle(menuName);            
            myAuctionMenu.DisplayBar(70,'=');
            
            int index = 0;
            // Find all advertised products
            foreach (var product in advertisedProducts){
                index++;
                Console.WriteLine($"Item {index})\tType: {product.ProductType}\tName: {product.ProductName}\tInitial Cost: {product.InitialPrice:C2}");
                if (index != advertisedProducts.Count)
                    myAuctionMenu.DisplayBar(70, '-');
            }

            if (index == 0)
                Console.WriteLine("No Products Advertised.");
            
            myAuctionMenu.DisplayBar(70, '=');
            myAuctionMenu.Message("");
        }

        public void SearchProducts()
        // Search for products on the market
        {
            int selection = 0;
            // loop unless back to menu option is selected (3).
            while (selection != 3){
                string menuName = "Search Products";
                myAuctionMenu.DisplaySubMenuTitle(menuName);

                string currentUserEmail = Program.ActiveClient.Email;
                string inputType = myAuctionMenu.GetStringInput("Please enter type to seach by");
                string buyFromEmail = string.Empty;
                
                myAuctionMenu.DisplayBar(70, '=');
                int index = 0;
                var myTypeSearchList = new List<Product>();

                //Search by type thourgh all clients and their advertised products.
                foreach (var client in Program.ClientList){

                    foreach (var product in client.MyAuctionHouse.AdvertisedProducts){
                        if (product.ProductType == inputType){
                            index++;
                            myTypeSearchList.Add(product);
                            Console.WriteLine($"Item {index})\tName: {product.ProductName}\tType: {product.ProductType}\tSeller: {product.SellerEmail}");
                        }
                    }
                }                          
                // If no items found in search
                if (myTypeSearchList.Count == 0){
                    Console.WriteLine("No Items Found.");
                    myAuctionMenu.DisplayBar(70, '=');
                    selection = myAuctionMenu.GetIntInput("1)Seach Again\t2)Back to Menu");
                    if (selection == 2)
                        break;
                }                
                else{
                    myAuctionMenu.DisplayBar(70, '=');
                    Console.WriteLine();
                    selection = BidOptionsMenu(selection,myTypeSearchList,currentUserEmail);              
                }
            }                               
        }    

        public int BidOptionsMenu(int selection, List<Product> searchList, string currentUserEmail)
        // Bid options menu - Sets options in bid menu and returns or makes a bid.
        {
            selection = myAuctionMenu.GetIntInput("1)Bid On Item\t2)Seach Again\t3)Back to Menu");
            switch (selection){
                case 1:
                    // sub bid options                    
                    int itemSelection = -1;
                    while (itemSelection < 1 || itemSelection > searchList.Count){
                        itemSelection = myAuctionMenu.GetIntInput($"\nWhich 'Item' did you want to bid on?" +
                            $"\nItem:(1) to Item:({searchList.Count})");
                    }
                    if (currentUserEmail != searchList[itemSelection - 1].SellerEmail){
                        Product currentItem = searchList[itemSelection - 1];                        
                        double inputAmount = myAuctionMenu.GetDoubleInput("\nHow much would you like to bid in $");
                        currentItem.MakeBid(BidDeliveryOptions(currentItem),currentUserEmail,inputAmount);
                    }
                    else
                        myAuctionMenu.Message("\nYou cant bid on your own item.");

                    Console.WriteLine();
                    selection = myAuctionMenu.GetIntInput("\nWhat would you Like to?\n1)Seach again, 2)Back to Menu") + 1;
                    break;
                case 2:
                    break;
                default:
                    break;
            }
            return selection;
        }
        public bool BidDeliveryOptions(Product item)
        // Prompts user for delivery options and returns as bool.
            {
            int deliveryType = 0;                        
            while (deliveryType >= 3 || deliveryType <= 0)
            {
            deliveryType = myAuctionMenu.GetIntInput("\n1)For Home Delivery\t2)Click And Collect");
            }
            bool clickAndCollect = deliveryType - 1 == 1;
            return clickAndCollect;
        }
        public void DisplayBids()
        // Display all bids on all clients items
        {            
            myAuctionMenu.DisplayBar(70, '=');
            int index = 0;
            // If no producs found display that outcome.
            if (advertisedProducts.Count == 0)
                Console.WriteLine("No Products Advertised.");
            
            // Integrate all bids in product and display
            foreach (var item in advertisedProducts)
            {
                index++;
                Console.WriteLine($"Item {index})\tType: {item.ProductType}\tName:{item.ProductName}\tInitial Price: {item.InitialPrice:c2}");
                if (item.BidsList.Count != 0){

                    foreach (var bid in item.BidsList){
                        Console.WriteLine($"\t{bid.Email}\t{bid.BidAmount:C2}");
                    }
                }
                else
                    Console.WriteLine("\tNo Bids");
                // only draw segragation bar if not last item in list
                if (index != advertisedProducts.Count)
                    myAuctionMenu.DisplayBar(70, '-');
            }
            myAuctionMenu.DisplayBar(70, '=');
        }
        
        public void ListBids()
        // Lists all current bids.
        {
            string menuName = "List Current Bids";
            myAuctionMenu.DisplaySubMenuTitle(menuName);
            DisplayBids();
            myAuctionMenu.Message();
        }

        public void SellProduct()
        // Sell A Product entry point from menu.
        {
            string menuName = "Sell A Product";
            myAuctionMenu.DisplaySubMenuTitle(menuName);
            
            DisplayBids();
            SellSubMenu(menuName);           
        }
    
        public int SelectItemToSell()
        // Selects the item to sell and returns the index.        
        {            
            int sellItemIndex = 0;
            // display menu options when items are available
            if (advertisedProducts.Count >= 1)
            {
                sellItemIndex = myAuctionMenu.GetIntInput($"Which 'Item' did you want to sell?" +
                $"\nItem: 1) to Item: {advertisedProducts.Count}) or ({advertisedProducts.Count + 1}) to go Back") - 1;
            }
            else
            {
                sellItemIndex = myAuctionMenu.GetIntInput($"({advertisedProducts.Count + 1}) to go Back") - 1;
            }
            return sellItemIndex;
        }

        public void SellSubMenu(string menuName)
        // Displays and controls the items selected to sell.
        {
            /* Checks if products are Advertised and handles subroutines to sell item
               to the highest bidder.
               else displays an error message.*/
            if (advertisedProducts.Count != 0){
                while (true){                    
                    Console.WriteLine();
                    int sellItemIndex = SelectItemToSell();                   
                                        
                    // Check for back to main menu or invalid choices and display.
                    if (sellItemIndex == advertisedProducts.Count)
                        break;

                    else if (sellItemIndex > advertisedProducts.Count){
                        myAuctionMenu.Message("Invalid Choice.");
                        myAuctionMenu.DisplaySubMenuTitle(menuName);
                        DisplayBids();
                    }
                    
                    else if (sellItemIndex < 0){
                        myAuctionMenu.Message("Invalid Choice.");
                        myAuctionMenu.DisplaySubMenuTitle(menuName);
                        DisplayBids();
                    }

                    else{
                        SellItemToBidder(sellItemIndex,menuName);
                    }
                }
            }
            else
                myAuctionMenu.Message("\nNo Items To Sell.");
        }
        
        public void SellItemToBidder(int sellItemIndex,string menuName)
        // Sells item to highest bidder.
        {
            // If bids are in list sell to highest bidder.
            // No bids found display outcome No Bids Message.
            if (advertisedProducts[sellItemIndex].BidsList.Count == 0){
                myAuctionMenu.Message("\nNo Bids On Item.");
                myAuctionMenu.DisplaySubMenuTitle(menuName);
                DisplayBids();
            }
            // Sell to highest bidder is checked if true continue with sale.
            else{
                string check = myAuctionMenu.GetStringInput("Are you sure (Y)es or (N)o").ToUpper();
                if (check == "Y"){
                    Bid winningBid = FindHighestBidder(sellItemIndex);
                    Product soldItem = soldProducts[SoldProducts.Count - 1];

                    DisplaySale(winningBid, soldItem);
                    myAuctionMenu.DisplaySubMenuTitle(menuName);
                    DisplayBids();
                }
                else{
                    myAuctionMenu.DisplaySubMenuTitle(menuName);
                    DisplayBids();
                }
            }
        }

        public void DisplaySale(Bid winningBid, Product soldItem)
        // Displays details of the sale.
        {
            Console.WriteLine();
            myAuctionMenu.DisplayBar(70, '-');

            winningBid.SellerCharge.GetExtraCostings();
            winningBid.SellerCharge.DisplayCharges();

            Console.WriteLine($"Item: {soldItem.ProductName} Sold to {winningBid.Email} for {winningBid.BidAmount:c2}");
            myAuctionMenu.DisplayBar(70, '-');
            
            myAuctionMenu.Message();
        }
        
        public Bid FindHighestBidder(int sellItemIndex)
        // Find the highest bid and returns the winnind Bid details.
        {
            double highestBid = 0;
            Bid winningBid = null;

            // search and check all bids for the highest bidder and save in winningBid.
            foreach (var bid in advertisedProducts[sellItemIndex].BidsList){
                if (bid.BidAmount > highestBid){
                    highestBid = bid.BidAmount;
                    winningBid = bid;
                }
            }
            soldProducts.Add(advertisedProducts[sellItemIndex]);            
            advertisedProducts.Remove(advertisedProducts[sellItemIndex]);
            return winningBid;
        }  
    }
}