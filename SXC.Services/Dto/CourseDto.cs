using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SXC.Services.Dto
{
    public class CourseDto
    {
        public int id { get; set; }

        public string name { get; set; }

        public string picurl { get; set; }

        public int? articleid { get; set; }
    }
}
