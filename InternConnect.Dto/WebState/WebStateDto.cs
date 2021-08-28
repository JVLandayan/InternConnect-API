namespace InternConnect.Dto.WebState
{
    public class WebStateDto
    {
        public class AddWebState
        {
            public string LogoFileName { get; set; }
            public string CoverPhotoFileName { get; set; }
        }

        public class UpdateWebState
        {
            public int Id { get; set; }
            public string LogoFileName { get; set; }
            public string CoverPhotoFileName { get; set; }
        }

        public class ReadWebState
        {
            public int Id { get; set; }
            public string LogoFileName { get; set; }
            public string CoverPhotoFileName { get; set; }
        }
    }
}