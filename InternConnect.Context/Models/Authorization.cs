using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternConnect.Context.Models
{
    public class Authorization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public List<Admin> Admins { get; set; }

    }
}
