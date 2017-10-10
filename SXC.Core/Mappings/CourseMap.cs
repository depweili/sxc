using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using SXC.Core.Models;

namespace SXC.Core.Mappings
{
    public class CourseMap : EntityTypeConfiguration<Course>
    {
        public CourseMap()
        {
            this.Property(t => t.Name).IsRequired().HasMaxLength(50);
            this.Property(t => t.Desc).HasMaxLength(50);
            this.Property(t => t.Pic).HasMaxLength(50);
            this.Property(t => t.Content).HasMaxLength(1000);
            this.Property(t => t.Price).HasColumnType("MONEY");
            this.Property(t => t.Period).HasMaxLength(20);
        }
    }
}
