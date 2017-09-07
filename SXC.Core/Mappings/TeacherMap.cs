using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using SXC.Core.Models;

namespace SXC.Core.Mappings
{
    class TeacherMap : EntityTypeConfiguration<Teacher>
    {
        public TeacherMap()
        {
            this.Property(t => t.Name).IsRequired().HasMaxLength(50);
            this.Property(t => t.Introduction).HasMaxLength(1000);
            this.Property(t => t.Pic).HasMaxLength(50);
            this.Property(t => t.Title).HasMaxLength(50);
            this.Property(t => t.IdentifyCode).HasMaxLength(50);
            this.Property(t => t.Character).HasMaxLength(200);
        }
    }
}
