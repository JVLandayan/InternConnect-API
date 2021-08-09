using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternConnect.Context.Models
{
    public class Content
    {
        public int Id { get; set; }

        public int StateId  { get; set; }
        public WebState WebState { get; set; }

        public List<Company> Company { get; set; }



    }
}
