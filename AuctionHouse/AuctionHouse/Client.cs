using System;


namespace AuctionHouse
{
    public class Client
    {
        private string name;
        private string email;
        private string address;
        private string password;
        private AuctionHouse myAuctionHouse;

        public string Name
        {
            get
            {
                return name;
            }

            private set
            {
                name = value;
            }
        }
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
        public string Address
        {
            get
            {
                return address;
            }

            private set
            {
                address = value;
            }
        }

        public AuctionHouse MyAuctionHouse
        {
            get
            {
                return myAuctionHouse;
            }
            private set
            {
                myAuctionHouse = value;
            }
        }

        public Client(string name, string email, string address, string password)
        // Constructor take name of the new client, their email address, street address and password.
        {
            MyAuctionHouse = new AuctionHouse();
            this.name = name;
            this.email = email;
            this.address = address;
            this.password = password;
        }           

        public bool PasswordCheck(string inputPassword)
        //  Checks if the password is equal to what is in list and returns via a bool
        {
            if (inputPassword == password)
                return true;
            else
                return false;
        }
    }
}