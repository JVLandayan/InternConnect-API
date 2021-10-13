using InternConnect.Dto.Company;
using System.ComponentModel.DataAnnotations;

namespace InternConnect.Dto.Opportunity
{
    public class OpportunityDto
    {
        public class ReadOpportunity
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Position { get; set; }
            public string Introduction { get; set; }
            public int CompanyId { get; set; }
            public CompanyDto.ReadCompany Company { get; set; }
        }

        public class UpdateOpportunity
        {
            [Required]
            public int Id { get; set; }
            [Required]
            public string Title { get; set; }
            [Required]
            public string Position { get; set; }
            public string Introduction { get; set; }
            [Required]
            public int CompanyId { get; set; }
        }

        public class AddOpportunity
        {
            [Required]
            public string Title { get; set; }
            [Required]
            public string Position { get; set; }
            public string Introduction { get; set; }
            [Required]
            public int CompanyId { get; set; }
        }
    }
}