using System.Collections.Generic;

namespace InternConnect.Context.Models
{
    public class Section
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ProgramId { get; set; }
        public List<Student> Students { get; set; }
        public List<Admin> Admins { get; set; }
    }
}