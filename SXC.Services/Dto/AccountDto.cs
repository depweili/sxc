using Newtonsoft.Json;
using SXC.Code.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SXC.Services.Dto
{
    public class UserAccountDto
    {
        public decimal Balance { get; set; }

        public string BankCard { get; set; }

        public bool IsValid { get; set; }

        public bool IsVerified { get; set; }
    }

    public class AccountRecordDto
    {
        public string AccountRecordSn { get; set; }

        public int Type { get; set; }

        public string ShortMark { get; set; }

        public decimal Amount { get; set; }

        public decimal AfterBalance { get; set; }

        public decimal BeforeBalance { get; set; }

        [JsonConverter(typeof(CommonDateTimeConverter))]
        public DateTime CreateTime { get; set; }

        public string Memo { get; set; }
    }

    public class AccountWithdrawDto
    {
        public Guid AuthID { get; set; }

        public decimal Amount { get; set; }

        public string BankCard { get; set; }
    }
}
