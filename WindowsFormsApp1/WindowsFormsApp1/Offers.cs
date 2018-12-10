using System;
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
    }
}
