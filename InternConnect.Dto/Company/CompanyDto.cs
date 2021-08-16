using InternConnect.Context.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternConnect.Dto.Company
{
    public class CompanyDto
    {
        public class ReadCompany
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

        public class UpdateCompany
        {
            [Required]
            public string Name { get; set; }
            [Required]
            public string AddressOne { get; set; }
            public string AddressTwo { get; set; }
            public string AddressThree { get; set; }
            [Required]
            public string City { get; set; }

            public string HeaderFileName { get; set; }
            public string LogoFileName { get; set; }
            public string Description { get; set; }

        }

        public class AddCompany
        {
            [Required]
            public string Name { get; set; }
            [Required]
            public string AddressOne { get; set; }
            public string AddressTwo { get; set; }
            public string AddressThree { get; set; }
            [Required]
            public string City { get; set; }

            public string HeaderFileName { get; set; }
            public string LogoFileName { get; set; }
            public string Description { get; set; }

        }

    }
}
