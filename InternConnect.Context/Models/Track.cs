namespace InternConnect.Context.Models
{
    public class Track
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int ProgramId { get; set; }
        public Program Programs { get; set; }
    }
}