using System;
using System.Collections.Generic;

namespace AuctionHouse
{
    public class Product
    {
        private string sellerEmail;
        private string productName;
        private string productType;
        private double initialPrice;
        private List<Bid> bidsList;
        public string SellerEmail
        {
            get
            {
                return sellerEmail;
            }
            private set
            {
                sellerEmail = value;
            }
        }
        public string ProductName
        {
            get
            {
                return productName;
            }
            private set
            {
                productName = value;
            }
        }
        public string ProductType
        {
            get
            {
                return productType;
            }
            private set
            {
                productType = value;
            }
        }
        public double InitialPrice
        {
            get
            {
                return initialPrice;
            }
            private set
            {
                initialPrice = value;
            }
        }
        public List<Bid> BidsList
        {
            get
            {
                return bidsList;
            }
            private set
            {
                bidsList = value;
            }
        }

        public Product(string sellerEmail, string productName,string productType ,double initialPrice)
        // Constructor takes the sellersEmail, name of the product, type of product and how much it costs initialy.
        {            
            bidsList = new List<Bid>();
            this.sellerEmail = sellerEmail;
            this.productName = productName;
            this.productType = productType;
            this.initialPrice = initialPrice;
        }

        public void MakeBid(bool clickAndCollect, string bidderEmail, double bidAmount)
        // handles the bid making uses the client email as the key.
        {
            if (BidsList.Count != 0){
                int index = 0;

                // Look for current bids using the Clients Email
                foreach (var bidder in BidsList){
                    
                    // Check if client already exists and update bid if so.
                    if (bidder.Email == bidderEmail){                                            
                        bidder.UpdateBid(bidAmount);
                        bidder.SaleTax.UpdateCollectionMethod(clickAndCollect);
                        bidder.SaleTax.GetExtraCostings();
                        break;
                    }
                    index++;
                }
                // if email not found in BidList add new Bid to BidList
                if (index == bidsList.Count){
                    bidsList.Add(new Bid(bidderEmail, bidAmount, clickAndCollect));
                    Console.WriteLine("\nYour Bid Has Been Added.");
                }
            }
            // if BidList is empty add new bid.
            else{
                bidsList.Add(new Bid(bidderEmail, bidAmount, clickAndCollect));
                Console.WriteLine("\nYour Bid Has Been Added.");
            }          
            Program.MyMenu.DisplayBar(70, '-');
            bidsList[BidsList.Count - 1].SaleTax.DisplayCharges();
            Program.MyMenu.DisplayBar(70, '-');
        }
    }
}