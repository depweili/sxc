using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SXC.Core.Models
{
    public class Article
    {
        public Article()
        {
            IsValid = true;
            //UID = Guid.NewGuid();
            CreateTime = DateTime.Now;
        }
        public int ID { get; set; }

        //public Guid UID { get; set; }

        public string Code { get; set; }

        public int Type { get; set; }

        public string Author { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public Nullable<bool> IsValid { get; set; }

        public DateTime CreateTime { get; set; }
        
    }
}
