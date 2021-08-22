using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            public int ProgramId { get; set; }

        }

        public class UpdateSection
        {
            [Required]
            public int Id { get; set; }
            [Required]
            public string Name { get; set; }
            [Required]
            public int ProgramId { get; set; }

        }

        public class AddSection
        {
            public string Name { get; set; }
            public int ProgramId { get; set; }
        }
    }
}
