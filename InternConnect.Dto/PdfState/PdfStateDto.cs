using System.ComponentModel.DataAnnotations;

namespace InternConnect.Dto.PdfState
{
    public class PdfStateDto
    {
        public class ReadPdfState
        {
            public int Id { get; set; }
            public string IgaarpName { get; set; }
            public string DeanName { get; set; }
            public string UstLogoFileName { get; set; }
            public string CollegeLogoFileName { get; set; }
        }

        public class UpdatePdfState
        {
            [Required] public int Id { get; set; }

            [Required] public string IgaarpName { get; set; }

            [Required] public string DeanName { get; set; }

            [Required] public string UstLogoFileName { get; set; }

            [Required] public string CollegeLogoFileName { get; set; }
        }

        public class AddPdfState
        {
            [Required] public string IgaarpName { get; set; }

            [Required] public string DeanName { get; set; }

            [Required] public string UstLogoFileName { get; set; }

            [Required] public string CollegeLogoFileName { get; set; }
        }
    }
}