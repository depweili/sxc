using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SXC.Services.Dto
{
    public class BusinessResultDto
    {
        public BusinessResultDto()
        {
            issave = true;
            errors = new List<string>();
        }
        public int status { get; set; }

        public string message { get; set; }

        public bool issave { get; set; }

        public dynamic detail { get; set; }

        public List<string> errors { get; set; }
    }
}
