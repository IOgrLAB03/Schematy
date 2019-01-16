using System;

namespace WindowsFormsApp1
{
    public class Offer
    {
        public Offer()
        {
        }

        public Offer(int id, DateTime date, DateTime? endDate, string title, string content, double basePrice,
            string place, string purpose)
        {
            Id = id;
            Date = date;
            EndDate = endDate;
            Title = title;
            Content = content;
            BasePrice = basePrice;
            Place = place;
            Purpose = purpose;
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