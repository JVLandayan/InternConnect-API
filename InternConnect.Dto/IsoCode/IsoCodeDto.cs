using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternConnect.Dto.Admin;
using InternConnect.Dto.Program;

namespace InternConnect.Dto.IsoCode
{
    public class IsoCodeDto
    {
        public class AddIsoCode
        {
            public int Code { get; set; }
            public int ProgramId { get; set; }
            public int AdminId { get; set; }
        }

        public class DeleteIsoCode
        {
            public int Id { get; set; }
            public int Code { get; set; }
            public int ProgramId { get; set; }
            public bool Used { get; set; }
            public int? SubmissionId { get; set; }
            public int AdminId { get; set; }

        }

        public class ReadIsoCode
        {
            public int Id { get; set; }
            public int Code { get; set; }
            public int ProgramId { get; set; }
            public ProgramDto.ReadProgram Program { get; set; }
            public bool Used { get; set; }
            public int? SubmissionId { get; set; }
            public int AdminId { get; set; }
            public AdminDto.ReadAdmin Admin { get; set; }
        }

        public class TransferIsoCode
        {
            public int Id { get; set; }
            public int AdminId { get; set; }
        }

        public class UpdateIsoCode
        {
            public int Id { get; set; }
            public int AdminId { get; set; }
            public bool Used { get; set; }
            public int? SubmissionId { get; set; }
        }
    }
}
