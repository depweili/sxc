using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using SXC.Core.Models;
using SXC.Code.Extensions;

namespace SXC.Core.Mappings
{
    class ArticleMap : EntityTypeConfiguration<Article>
    {
        public ArticleMap()
        {
            this.Property(t => t.Title).HasMaxLength(200);
            this.Property(t => t.Content).HasColumnType("ntext");
            this.Property(t => t.Code).HasMaxLength(20);
            this.Property(t => t.Author).HasMaxLength(40);
            //this.Property(t => t.CreateTime).IsRequired();

            //this.Map(t => t.Requires("CreateTime").HasValue<DateTime>(DateTime.Now));
        }
    }
}
