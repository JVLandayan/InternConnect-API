using InternConnect.Dto.Account;
using InternConnect.Dto.Program;
using InternConnect.Dto.Section;

namespace InternConnect.Dto.Admin
{
    public class AdminDto
    {
        public class UpdateAdmin
        {
            public string StampFileName { get; set; }
        }

        public class ReadAdmin
        {
            public int Id { get; set; }
            public string StampFileName { get; set; }
            public int AuthId { get; set; }
            public int? ProgramId { get; set; }
            public int? SectionId { get; set; }
            public int AccountId { get; set; }

            public SectionDto.ReadSection Section { get; set; }
            public ProgramDto.ReadProgram Program { get; set; }

        }

        public class ReadCoordinator
        {
            public int Id { get; set; }
            public string StampFileName { get; set; }
            public int AuthId { get; set; }
            public int? ProgramId { get; set; }
            public int? SectionId { get; set; }
            public int AccountId { get; set; }

            public SectionDto.ReadSection Section { get; set; }
            public ProgramDto.ReadProgram Program { get; set; }
            public AccountDto.ReadCoordinator Account { get; set; }
        }
    }
}