using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SXC.Services.Dto
{
    public class ArticleDto
    {
        public string author { get; set; }

        public string title { get; set; }

        public string content { get; set; }

        public DateTime? createtime { get; set; }
    }
}
