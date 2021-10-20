using System.ComponentModel.DataAnnotations;

namespace InternConnect.Dto.Company
{
    public class CompanyDto
    {
        public class ReadCompany
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Link { get; set; }
            public string AddressOne { get; set; }
            public string AddressTwo { get; set; }
            public string AddressThree { get; set; }
            public string City { get; set; }
            public string HeaderFileName { get; set; }
            public string LogoFileName { get; set; }
            public string Description { get; set; }
            public string ContactPersonName { get; set; }
            public string ContactPersonEmail { get; set; }
            public string ContactPersonDesignation { get; set; }
            public string Status { get; set; }
        }

        public class UpdateCompany
        {
            [Required] public int Id { get; set; }

            [Required] public string Name { get; set; }

            [Required] public string Link { get; set; }

            [Required] public string AddressOne { get; set; }

            public string AddressTwo { get; set; }
            public string AddressThree { get; set; }

            [Required] public string City { get; set; }

            public string HeaderFileName { get; set; }
            public string LogoFileName { get; set; }
            public string Description { get; set; }
            public string ContactPersonName { get; set; }
            public string ContactPersonEmail { get; set; }
            public string ContactPersonDesignation { get; set; }
        }

        public class UpdateCompanyStatus
        {
            [Required] public int Id { get; set; }
            [Required]public string Status { get; set; }
        }

        public class AddCompany
        {
            [Required] public string Name { get; set; }
            public string Link { get; set; }

            [Required] public string AddressOne { get; set; }

            public string AddressTwo { get; set; }
            public string AddressThree { get; set; }

            [Required] public string City { get; set; }

            public string HeaderFileName { get; set; }
            public string LogoFileName { get; set; }
            public string Description { get; set; }
            public string ContactPersonName { get; set; }
            public string ContactPersonEmail { get; set; }
            public string ContactPersonDesignation { get; set; }
        }
    }
}