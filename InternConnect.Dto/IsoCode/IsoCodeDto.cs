using System.ComponentModel.DataAnnotations;
using InternConnect.Dto.Admin;
using InternConnect.Dto.Program;

namespace InternConnect.Dto.IsoCode
{
    public class IsoCodeDto
    {
        public class AddIsoCode
        {
            [Required] public int Code { get; set; }

            [Required] public int ProgramId { get; set; }
        }

        public class DeleteIsoCode
        {
            [Required] public int Id { get; set; }

            [Required] public int Code { get; set; }

            [Required] public int ProgramId { get; set; }

            [Required] public bool Used { get; set; }

            [Required] public int? SubmissionId { get; set; }

            [Required] public int AdminId { get; set; }
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
            public string AdminEmail { get; set; }
        }

        public class TransferIsoCode
        {
            [Required] public int Id { get; set; }

            [Required] public int Code { get; set; }

            [Required] public int ProgramId { get; set; }
        }

        public class UpdateIsoCode
        {
            [Required] public int Id { get; set; }

            [Required] public int AdminId { get; set; }

            [Required] public bool Used { get; set; }

            [Required] public int? SubmissionId { get; set; }
        }
    }
}