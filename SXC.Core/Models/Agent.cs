using SXC.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SXC.Core.Models
{
    public class Agent
    {
        public Agent()
        {
            IsValid = true;
            CreateTime = DateTime.Now;
            AgentID = Guid.NewGuid();
            Code = Cryptography.GetRandomString(8);
        }

        public int ID { get; set; }

        public int? PID { get; set; }

        public Guid AgentID { get; set; }

        public string Code { get; set; }

        public int Type { get; set; }

        public int Level { get; set; }

        public int State { get; set; }

        public decimal Commission { get; set; }

        public Nullable<bool> IsValid { get; set; }

        public Nullable<DateTime> CreateTime { get; set; }

        public Nullable<DateTime> SupAgentBindTime { get; set; }

        public virtual Agent ParentAgent { get; set; }

        public virtual ICollection<Agent> ChildAgents { get; set; }

        public virtual Base_Area Area { get; set; }

        public virtual User User { get; set; }
    }
}
