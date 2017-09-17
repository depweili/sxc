using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SXC.Core.Models
{
    public class Cooperation
    {
        public Cooperation()
        {
            CreateTime = DateTime.Now;
        }

        public int ID { get; set; }

        public string Name { get; set; }

        public string MobilePhone { get; set; }

        public string AreaInfo { get; set; }

        public string Address { get; set; }

        public int Type { get; set; }

        public int Level { get; set; }

        public string AgentAreaInfo { get; set; }

        public string Memo { get; set; }

        //public int State { get; set; }

        public string ProcessDetail { get; set; }

        public DateTime CreateTime { get; set; }

        public int UserID { get; set; }
        public virtual User User { get; set; }

        public int? AreaID { get; set; }
        public virtual Base_Area Area { get; set; }
    }
}
