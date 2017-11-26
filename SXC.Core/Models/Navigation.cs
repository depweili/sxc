using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SXC.Core.Models
{
    public class Navigation
    {
        public Navigation()
        {
            IsValid = true;
            CreateTime = DateTime.Now;
        }

        public int ID { get; set; }

        public int Type { get; set; }

        public string Desc { get; set; }

        public string Pic { get; set; }

        public string Target { get; set; }

        public int Order { get; set; }

        public Nullable<bool> IsValid { get; set; }

        public DateTime CreateTime { get; set; }

        public int? ArticleID { get; set; }

        public virtual Article Article { get; set; }

    }
}
