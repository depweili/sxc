using SXC.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SXC.Core.Models
{
    public class User
    {
        public User()
        {
            IsValid = true;
            CreateTime = DateTime.Now;
            AuthID = Guid.NewGuid();
            UserName = Cryptography.GetRandomString(8);
        }
        public int ID { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public Guid AuthID { get; set; }

        public Nullable<bool> IsValid { get; set; }

        public DateTime? LastActiveTime { get; set; }

        public DateTime CreateTime { get; set; }

        public string SystemAccount { get; set; }

        public virtual UserProfile UserProfile { get; set; }

        public virtual Agent Agent { get; set; }

        public virtual UserIntegral UserIntegral { get; set; }

        public virtual UserAccount UserAccount { get; set; }
    }

    public class UserProfile
    {
        public UserProfile()
        {
            IsVerified = false;
            CreateTime = DateTime.Now;
        }
        public int ID { get; set; }

        public string NickName { get; set; }

        public string AvatarUrl { get; set; }

        public string RealName { get; set; }

        public Nullable<int> Gender { get; set; }

        public Nullable<int> Age { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string MobilePhone { get; set; }

        public string IDCard { get; set; }

        //public string AgentCode { get; set; }

        //public bool IsAgent { get; set; }

        public Nullable<DateTime> CreateTime { get; set; }

        public Nullable<bool> IsVerified { get; set; }

        public virtual User User { get; set; }
    }

    public class UserAuth
    {
        public UserAuth()
        {
            IsValid = true;
            CreateTime = DateTime.Now;
        }
        public int ID { get; set; }

        public string IdentityType { get; set; }

        public string Identifier { get; set; }

        public string Credential { get; set; }

        public Nullable<DateTime> CreateTime { get; set; }

        public Nullable<bool> IsValid { get; set; }

        public Nullable<DateTime> LastActiveTime { get; set; }

        public virtual User User { get; set; }
    }
}
