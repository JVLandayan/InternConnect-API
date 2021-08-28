using System.Collections.Generic;

namespace InternConnect.Context.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AddressOne { get; set; }
        public string AddressTwo { get; set; }
        public string AddressThree { get; set; }
        public string City { get; set; }
        public string HeaderFileName { get; set; }
        public string LogoFileName { get; set; }
        public string Description { get; set; }
        public List<Opportunity> Opportunities { get; set; }
    }
}