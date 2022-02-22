using System;


namespace AuctionHouse
{
    public class AuctionCharge : ICharge
    {
        private double amount;
        private bool clickAndCollect;
       
        public double Amount
        {
            get
            {
                return amount;
            }
            private set
            {
                amount = value;
            }
        }
        public bool ClickAndCollect
        {
            get
            {
                return clickAndCollect;
            }
            private set
            {
                clickAndCollect = value;
            }
        }

        public AuctionCharge(bool clickAndCollect)
        // Constructor works out extra costing based on delivery method.
        {
            this.clickAndCollect = clickAndCollect;
            GetExtraCostings();
        }
        public void GetExtraCostings()
        // calculates the extra costs for the auction due to delivery type.
        {
            if (ClickAndCollect)
            {
                amount = 10;
            }
            else
            {
                amount = 20;
            }
        }

        public void UpdateCollectionMethod(bool clickAndCollect)
        // calculates the extra costs for the auction due to delivery type.
        {
            this.clickAndCollect = clickAndCollect;
        }
        public void DisplayCharges()
        // Displays the auction charges to screen.
        {
            Console.WriteLine($"Auction Charges: {amount:c2}");
        }

    }
}