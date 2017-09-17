using Newtonsoft.Json;
using SXC.Code.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SXC.Services.Dto
{
    public class IntegralDto
    {
        public string message { get; set; }

        public dynamic detail { get; set; }
    }

    public class IntegralRecordDto
    {
        public int points { get; set; }

        public int totalpoints { get; set; }

        public int currentpoints { get; set; }
    }
}
