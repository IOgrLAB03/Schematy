using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Offer
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public double BasePrice { get; set; }
        public string Place { get; set; }
        public string Purpose { get; set; }
        public DateTime Date { get; set; }


        public void AddSample()
        {
            using (var db = new LiteDatabase(@"Offers.db"))
            {
                var offers = db.GetCollection<Offer>("offers");
                var offer = new Offer
                {
                    Title = "Wakacje",
                    Content = "Wyjazd",
                    BasePrice = 4.123,
                    Place = "Krakow",
                    Purpose = "Wyjzad sluzbowy",
                    Date = new DateTime(2018, 12, 21)
                };

                offers.Insert(offer);
            }
        }

        public void addOffer(Offer offer)
        {
            using (var db = new LiteDatabase(@"Offers.db"))
            {
                var offers = db.GetCollection<Offer>("offers");
                offers.Insert(offer);
            }
        }
    }
}

