using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SXC.Core.Models
{
    public class Course
    {
        public Course()
        {
            IsValid = true;
            CreateTime = DateTime.Now;
        }

        public int ID { get; set; }

        public string Name { get; set; }

        public string Desc { get; set; }

        public string Pic { get; set; }

        public string Content { get; set; }

        public decimal? Price { get; set; }

        public double? Period { get; set; }

        public bool? HasVideo { get; set; }

        public bool? HasFreeVideo { get; set; }

        public int Order { get; set; }

        public Nullable<bool> IsValid { get; set; }

        public DateTime CreateTime { get; set; }

        public int? ArticleID { get; set; }

        public virtual Article Article { get; set; }

        

    }
}
