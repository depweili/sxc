using SXC.Code;
using SXC.Code.Utility;
using SXC.Core.Data;
using SXC.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SXC.Code.Extensions;
using SXC.Code.Cache;

namespace SXC.Services.Business
{
    public class AccountBus : BusinessBase
    {
        private string _message;

        private string _error;

        private bool _isSave;

        public AccountBus(SxcDbContext DbContext)
            : base(DbContext)
        {
            _isSave = true;
        }
        public void SaveRecord(AccountRecord record)
        {
            try
            {

            }
            catch (Exception ex)
            {
                
                throw;
            }
        }
        public void ShareReward(UserAccount useracc, User user)
        {
            decimal redmoney = GetShareReward();

            var newrecord = new AccountRecord
            {
                Amount = redmoney,
                AfterBalance = useracc.Balance + redmoney,
                BeforeBalance = useracc.Balance,
                UserAccount = useracc,
                ShortMark = "分享红包",
                Type = 1,
                Memo = user.UserProfile.NickName
            };

            useracc.Balance = newrecord.AfterBalance;
            _context.AccountRecords.Add(newrecord);
        }

        private decimal GetShareReward()
        {
            decimal res = 0.2m;
            string key = "CacheShareReward";
            try
            {
                if (CacheHelper.Exist(key))
                {
                    res = CacheHelper.Get<decimal>("ShareReward");
                }
                else
                {
                    var value = Cryptography.Base64ForUrlDecode(ConfigHelper.GetConfig("ShareReward"));
                    res = value.ToDecimal();
                    CacheHelper.Set(key, res);
                }
                

                
            }
            catch (Exception)
            {
                res = 0.2m;
                CacheHelper.Set(key, res);
            }
            

            return res;
        }

        
    }
}
