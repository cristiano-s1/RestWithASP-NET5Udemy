using System;

namespace RestWithASPNETUdemy.Model
{
    public class Book
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public DateTime LaunchDate { get; set; }
        public Decimal Price { get; set; }
        public string Title { get; set; }
    }
}
