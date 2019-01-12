using System;
using LiteDB;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Offer
    {
        public Offer()
        {

        }

        public Offer(int id, DateTime date, DateTime? endDate, string title, string content, double basePrice, string place, string purpose)
        {
            this.Id = id;
            this.Date = date;
            this.EndDate = endDate;
            this.Title = title;
            this.Content = content;
            this.BasePrice = basePrice;
            this.Place = place;
            this.Purpose = purpose;
        }

        public DateTime? EndDate { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public double BasePrice { get; set; }
        public string Place { get; set; }
        public string Purpose { get; set; }
        public DateTime Date { get; set; }

        public void addOffer(Offer offer)
        {
            using (var db = new LiteDatabase(@"Offers.db"))
            {
                var offers = db.GetCollection<Offer>("offers");

                if (offers.FindById(offer.Id) == null)
                {
                    offers.Insert(offer);
                }
                else
                {
                    offers.Update(offer);
                }

            }
        }

        public Offer getOffer(int ID)
        {
            using (var db = new LiteDatabase(@"Offers.db"))
            {
                var offers = db.GetCollection<Offer>("offers");
                return offers.FindById(ID);
            }
        }

        public bool deleteOffer(int ID)
        {
            using (var db = new LiteDatabase(@"Offers.db"))
            {
                var offers = db.GetCollection<Offer>("offers");
                return offers.Delete(ID);
            }
        }

        public bool updateOffer(Offer offer)
        {
            using (var db = new LiteDatabase(@"Offers.db"))
            {
                var offers = db.GetCollection<Offer>("offers");
                return offers.Update(offer);
            }
        }


    }
}
