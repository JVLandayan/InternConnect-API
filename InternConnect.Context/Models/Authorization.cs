using System.Collections.Generic;

namespace InternConnect.Context.Models
{
    public class Authorization
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Admin> Admins { get; set; }
    }
}