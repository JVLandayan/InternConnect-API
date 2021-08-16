using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternConnect.Dto.Section
{
    public class SectionDto
    {
        public class ReadSection
        {
            public int Id { get; set; }
            public string Name { get; set; }

        }

        public class UpdateSection
        {
            public int Id { get; set; }
            public string Name { get; set; }

        }

        public class AddSection
        {
            public string Name { get; set; }
        }
    }
}
