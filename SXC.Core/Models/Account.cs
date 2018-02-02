using SXC.Code.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SXC.Core.Models
{
    public class UserAccount
    {
        public UserAccount()
        {
            IsValid = true;
            IsVerified = false;
            CreateTime = DateTime.Now;
            AccountID = Guid.NewGuid();
        }
        public int ID { get; set; }

        public Guid AccountID { get; set; }

        public string PassWword { get; set; }

        public decimal Balance { get; set; }

        public decimal LockBalance { get; set; }

        public decimal Cash { get; set; }

        public decimal Expense { get; set; }

        public string BankCard { get; set; }

        public bool IsValid { get; set; }

        public DateTime CreateTime { get; set; }

        public bool IsVerified { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<AccountRecord> AccountRecords { get; set; }
    }

    public class AccountRecord
    {
        public AccountRecord()
        {
            AccountRecordSn = Constant.SNPREFIX_ACCOUNT + RandomHelper.GetTimeRandom1();
            CreateTime = DateTime.Now;
        }
        public int ID { get; set; }

        public string AccountRecordSn { get; set; }

        public int UserAccountID { get; set; }
        public virtual UserAccount UserAccount { get; set; }

        public int Type { get; set; }

        public string ShortMark { get; set; }

        public decimal Amount { get; set; }

        public decimal AfterBalance { get; set; }

        public decimal BeforeBalance { get; set; }

        public DateTime CreateTime { get; set; }

        public string Memo { get; set; }
    }

    public class AccountWithdraw
    {

        public AccountWithdraw()
        {
            CreateTime = DateTime.Now;
        }
        public int ID { get; set; }

        public int UserAccountID { get; set; }
        public virtual UserAccount UserAccount { get; set; }

        public decimal Amount { get; set; }

        public DateTime CreateTime { get; set; }

        public int State { get; set; }

        public DateTime? CompleteTime { get; set; }

        public string Memo { get; set; }

        public int AccountRecordID { get; set; }
        public virtual AccountRecord AccountRecord { get; set; }
    }

}
