using System;

namespace AuctionHouse
{
    public class Tax : ICharge
    {
        private double amount;
        private bool clickAndCollect;
        private double saleCost;
        private double taxPercentage = 0.15;
        private double deliveryCharge;

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

        public double SaleCost
        {
            get
            {
                return saleCost;
            }
            private set
            {
                saleCost = value;
            }
        }
        public double TaxPercentage
        {
            get
            {
                return taxPercentage;
            }
            private set
            {
                taxPercentage = value;
            }
        }
        public double DeliveryCharge
        {
            get
            {
                return deliveryCharge;
            }
            private set
            {
                deliveryCharge = value;
            }
        }

        public Tax(bool clickAndCollect, double saleCost)
        // Constructor works out extra costing based on delivery method and price of sale (saleCost).
        {
            this.saleCost = saleCost;
            this.clickAndCollect = clickAndCollect;
            GetExtraCostings();
        }

        public void GetExtraCostings()
        // calculates the extra costs for the auction due to delivery type.
        {
            if (ClickAndCollect){
                deliveryCharge = 0;
                amount = saleCost * taxPercentage;
            }
            else{
                deliveryCharge = 5;
                amount = (saleCost * taxPercentage) + deliveryCharge;
            }
        }

        public void UpdateCollectionMethod(bool clickAndCollect)
        // calculates the extra costs for the auction due to delivery type.
        {
            this.clickAndCollect = clickAndCollect;
        }

        public void DisplayCharges()
        // Displays Tax Charges and Delivery charge on sale of product to screen.
        {
            Console.WriteLine($"Tax Rate: {taxPercentage}%\t\tExtra Tax Charges: {deliveryCharge:C2}\nTotal Tax Charges Amount: {amount:c2}");
        }

    }
}