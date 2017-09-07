using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SXC.Services.Dto
{
    public class NavDto
    {
        public int id { get; set; }

        public string desc { get; set; }

        public string picurl { get; set; }

        public string target { get; set; }

        public int? articleid { get; set; }
    }
}
