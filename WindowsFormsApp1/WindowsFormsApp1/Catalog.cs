using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Properties;

namespace WindowsFormsApp1
{
    public class Catalog
    {
        public List<Offer> Offers { get; set; }
        public Random Random = new Random();

        public Catalog()
        {
            Offers = new List<Offer>();
            GetOffersFormDb();
            // Mock
            Offers.Add(new Offer(Random.Next(), DateTime.Now, null, "Offer 1", "Content'll be expand", 1000.0, "USA", "any"));
            Offers.Add(new Offer(Random.Next(), DateTime.Now, new DateTime(2019, 01, 15), "Offer 2", "Content'll be expand", 2000.0, "USA", "any"));

        }

        // TODO: from db get all offers
        public void GetOffersFormDb()
        {

            // TODO: will be something like that:
            //foreach (var VARIABLE in DB)
            //{
            //    Offers.Add(VARIABLE);
            //}
            // TODO: or something like that:
            // SELECT * FROM db
            // WHERE (EndDate >= DateTime.Now) OR (EndDate == NULL)



        }

        public List<ListViewItem> ReadOffers()
        {
            var listViewItems = new List<ListViewItem>();
            foreach (var offer in Offers)
            {
                listViewItems.Add(new ListViewItem(OfferListViewBody(offer)));
            }
            return listViewItems;
        }

        internal string[] OfferListViewBody(Offer offer)
        {
            return new[]
            {
                offer.Title,
                offer.Place,
                offer.BasePrice.ToString(CultureInfo.CurrentCulture),
                offer.Id.ToString()
            };
        }

        //TODO: Dodawanie oferty do bazy danych
        void addOfferToDb(Offer newOffer) 
        {

        }

        // TODO: aktualizowanie ofert w bazie
        void updateOfferInDb(int id)
        {

        }

        public void DeleteOfferFromList(int id)
        {
            Offers.Remove(Offers.Find(o => o.Id == id));
        }

        public void DeleteOfferFromList(Offer offer)
        {
            Offers.Remove(offer);
        }

        public Offer GetOfferById(int id)
        {
            return Offers.Find(o => o.Id == id);
        }



        // TODO: do wywalenia -> fronEnd to robi
        void readOffersById()
        {

        }

    }
}
