using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuotesForCarInsurance.Models
{
    public class Quotes
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string DateOfBirth { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Dui { get; set; }
        public string Tickets { get; set; }
        public string CoverOrLiaility { get; set; }
        public string Year { get; set; }
    }
}