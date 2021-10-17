namespace InternConnect.Dto
{
    public class ChangeDeanModel
    {
        public string OldEmail { get; set; }
        public string NewEmail { get; set; }
        public string Password { get; set; }
    }

    public class CompanyAndNumberOfStudentModel
    {
        public int CompanyId { get; set; }
        public int NumberOfOccurence { get; set; }
    }
}