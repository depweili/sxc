using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SXC.Core.Models
{
    public class Teacher
    {
        public Teacher()
        {
            IsValid = true;
        }
        public int ID { get; set; }

        public string Name { get; set; }

        public string IdentifyCode { get; set; }

        public int Type { get; set; }

        public string Title { get; set; }

        public string Introduction { get; set; }

        public string Character { get; set; }

        public string Pic { get; set; }

        public int Order { get; set; }

        public Nullable<bool> IsValid { get; set; }

        public int? ArticleID { get; set; }

        public virtual Article Article { get; set; }


    }
}
