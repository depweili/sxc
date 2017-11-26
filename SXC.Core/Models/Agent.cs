using SXC.Code;
using SXC.Code.Utility;
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

    public class UserPayment
    {
        public UserPayment()
        {
            IsDistr = true;
            CreateTime = DateTime.Now;
            PayUID = Guid.NewGuid();
            PaySN = Constant.SNPREFIX_PAYMENT + RandomHelper.GetTimeRandom1();
        }

        public int ID { get; set; }

        public int UserID { get; set; }
        public virtual User User { get; set; }

        public int PayItemID { get; set; }
        public virtual PaymentItem PayItem { get; set; }

        public Guid PayUID { get; set; }

        public string PaySN { get; set; }

        public bool IsDistr { get; set; }

        public int DistrType { get; set; }

        public int State { get; set; }

        public decimal Amount { get; set; }

        public decimal DistrAmount { get; set; }

        public decimal Commission { get; set; }

        public decimal FinalAmount { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? DistrTime { get; set; }

        public DateTime? AccountTime { get; set; }

        public string Memo { get; set; }

        public string OperatorID { get; set; }

        public string OperatorName { get; set; }

        public virtual ICollection<CommissionRecord> CommissionRecords { get; set; }
    }

    public class CommissionRecord
    {
        public int ID { get; set; }

        public decimal Commission { get; set; }

        public int UserPaymentID{ get; set; }
        public virtual UserPayment UserPayment { get; set; }

        public int AgentID { get; set; }
        public virtual Agent Agent { get; set; }

        public int State { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? CheckTime { get; set; }

        public string Memo { get; set; }
        
    }

    public class PaymentItem
    {
        public int ID { get; set; }

        public string Name { get; set; }
    }



}
