using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Order
    {
        public double Amount { get; set; }
        public DateTime Departure { get; set; }
        public DateTime Homecomming { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int PersonCount { get; set; }
        public List<Client> Clients { get; set; }

        void GetData()
        {

        }

        void ProcessOffer()
        {

        }

        void ReadOrder()
        {

        }

        void ArchiveOrder()
        {

        }
    }

    
}
