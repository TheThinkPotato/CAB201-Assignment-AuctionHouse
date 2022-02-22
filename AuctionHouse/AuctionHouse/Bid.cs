using System;
using System.Collections.Generic;

namespace AuctionHouse
{
    public class Bid
    {
        private string email;
        private double bidAmount;
        private ICharge saleTax;
        private ICharge sellerCharge;
        public string Email
                {
            get
            {
                return email;
            }
            private set
            {
                email = value;
            }
        }
        public double BidAmount
        {
            get
            {
                return bidAmount;
            }
            private set
            {
                bidAmount = value;
            }
        }
        public ICharge SaleTax
        {
            get
            {
                return saleTax;
            }
            private set
            {
                saleTax = value;
            }
        }
        public ICharge SellerCharge
        {
            get
            {
                return sellerCharge;
            }
            private set
            {
                sellerCharge = value;
            }
        }

        public Bid(string email, double bidAmount, bool clickAndCollect)
        // Constructor takes bidders email address, amount the are bidding and if they are using click and collect.
        {
            this.email = email;
            this.bidAmount = bidAmount;
            saleTax = new Tax(clickAndCollect, bidAmount);
            sellerCharge = new AuctionCharge(clickAndCollect);
        }

        public void UpdateBid(double bidAmount)
        // Updates the Bid Amount with bidAmount.
        {
            this.bidAmount = bidAmount;
            Console.WriteLine("You bid has been updated.\n");
        }

    }
}