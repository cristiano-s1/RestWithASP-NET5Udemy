using System;
using RestWithASPNETUdemy.Model.Base;

namespace RestWithASPNETUdemy.Model
{
    public class Book : BaseEntity
    {
        public string Author { get; set; }
        public DateTime LaunchDate { get; set; }
        public Decimal Price { get; set; }
        public string Title { get; set; }
    }
}
