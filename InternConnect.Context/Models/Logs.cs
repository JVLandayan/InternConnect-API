using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternConnect.Context.Models
{
    public class Logs
    {
        public int Id { get; set; }
        public DateTime DateStamped { get; set; }
        public int AdminId { get; set; }
        public Admin Admin { get; set; }

    }
}
