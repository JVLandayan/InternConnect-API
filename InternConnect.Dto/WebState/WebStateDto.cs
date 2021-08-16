using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
