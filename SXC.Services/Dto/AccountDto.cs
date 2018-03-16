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
        [JsonConverter(typeof(DecimalDigitsConverter))]
        public decimal Balance { get; set; }

        public string BankCard { get; set; }

        public string BankName { get; set; }

        public string BranchBankName { get; set; }
        public string MobilePhone { get; set; }

        public bool IsValid { get; set; }

        public bool IsVerified { get; set; }
    }

    public class AccountRecordDto
    {
        public string AccountRecordSn { get; set; }

        public int Type { get; set; }

        public string ShortMark { get; set; }

        [JsonConverter(typeof(DecimalDigitsConverter))]
        public decimal Amount { get; set; }

        [JsonConverter(typeof(DecimalDigitsConverter))]
        public decimal AfterBalance { get; set; }

        [JsonConverter(typeof(DecimalDigitsConverter))]
        public decimal BeforeBalance { get; set; }

        [JsonConverter(typeof(CommonDateTimeConverter))]
        public DateTime CreateTime { get; set; }

        public string Memo { get; set; }
    }

    public class AccountWithdrawDto
    {
        public Guid AuthID { get; set; }

        [JsonConverter(typeof(DecimalDigitsConverter))]
        public decimal Amount { get; set; }

        public string Name { get; set; }

        public string BankCard { get; set; }

        public string BankName { get; set; }

        public string BranchBankName { get; set; }
        public string MobilePhone { get; set; }

        public DateTime CreateTime { get; set; }

        public int State { get; set; }

        public string Memo { get; set; }
    }
}
