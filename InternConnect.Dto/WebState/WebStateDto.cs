using System.ComponentModel.DataAnnotations;

namespace InternConnect.Dto.WebState
{
    public class WebStateDto
    {
        public class AddWebState
        {
            [Required] public string LogoFileName { get; set; }
            [Required] public string CoverPhotoFileName { get; set; }
        }

        public class UpdateWebState
        {
            [Required] public int Id { get; set; }
            [Required] public string LogoFileName { get; set; }
            [Required] public string CoverPhotoFileName { get; set; }
        }

        public class ReadWebState
        {
            public int Id { get; set; }
            public string LogoFileName { get; set; }
            public string CoverPhotoFileName { get; set; }
        }
    }
}