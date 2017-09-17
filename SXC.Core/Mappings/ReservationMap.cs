using SXC.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SXC.Core.Mappings
{
    public class ReservationMap : EntityTypeConfiguration<Reservation>
    {
        public ReservationMap()
        {
            this.Property(t => t.Address).HasMaxLength(50);
            this.Property(t => t.Memo).HasMaxLength(50);
            this.Property(t => t.MobilePhone).HasMaxLength(20);
            this.Property(t => t.Name).HasMaxLength(10);
            this.Property(t => t.ProcessDetail).HasMaxLength(500);
            this.Property(t => t.Purpose).HasMaxLength(40);
            this.Property(t => t.AreaInfo).HasMaxLength(20);
        }
    }

    public class ReservationCourseMap : EntityTypeConfiguration<ReservationCourse>
    {
        public ReservationCourseMap()
        {
        }
    }

    public class LearnPurposeMap : EntityTypeConfiguration<LearnPurpose>
    {
        public LearnPurposeMap()
        {
            this.Property(t => t.Purpose).HasMaxLength(20);
        }
    }
}
