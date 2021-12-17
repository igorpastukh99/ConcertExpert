namespace Expert.Models
{
    public class Concert
    {
        public int Id { get; set; }
        public string Style { get; set; }
        public string Date { get; set; }
        public string Venue { get; set; }
        public string Band { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int Price { get; set; }

        public Concert(string style, string date, string venue, string band, string country, string city, int price)
        {
            Style = style;
            Date = date;
            Venue = venue;
            Band = band;
            Country = country;
            City = city;
            Price = price;
        }
    }
}
