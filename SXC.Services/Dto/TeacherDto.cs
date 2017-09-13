using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SXC.Services.Dto
{
    public class TeacherDto
    {
        public int id { get; set; }

        public string name { get; set; }

        public string title { get; set; }

        public string picurl { get; set; }

        public string introduction { get; set; }

        public string character { get; set; }

        public int? articleid { get; set; }

    }
}
