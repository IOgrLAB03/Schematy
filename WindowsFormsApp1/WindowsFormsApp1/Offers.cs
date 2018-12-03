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

        public Offer(int id, DateTime date, string title, string content, double basePrice, string place, string purpose)
        {
            this.Id = id;
            this.Date = date;
            this.Title = title;
            this.Content = content;
            this.BasePrice = basePrice;
            this.Place = place;
            this.Purpose = purpose;
        }
    }
}

using (var db = new LiteDatabase("DataBase.db"))
{
    var col = db.GetCollection<Offer>("offers");
}