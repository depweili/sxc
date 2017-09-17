using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SXC.Services.Dto
{
    public class CooperationDto
    {
        public int id { get; set; }

        public Guid authid { get; set; }

        public string name { get; set; }

        public string mobilephone { get; set; }

        public string areainfo { get; set; }

        public string address { get; set; }

        public int type { get; set; }

        public int level { get; set; }

        public string agentareainfo { get; set; }

        public string memo { get; set; }

        public int? areaid { get; set; }
    }
}
