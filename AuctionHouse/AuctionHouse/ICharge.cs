using System;


namespace AuctionHouse
{
    public interface ICharge
    {
        void DisplayCharges();
        // Displays all chrages to screen - promise
        void GetExtraCostings();
        // Works out the charges costings - promise
        void UpdateCollectionMethod(bool clickAndCollect);        
        // Updates the type of collection (click&Collect or Delivery) - promise.
    }
}