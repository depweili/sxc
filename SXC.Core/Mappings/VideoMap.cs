using SXC.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SXC.Core.Mappings
{
    public class VideoSeriesMap : EntityTypeConfiguration<VideoSeries>
    {
        public VideoSeriesMap()
        {
            this.Property(t => t.Title).HasMaxLength(50);
            this.Property(t => t.Cover).HasMaxLength(100);
            this.Property(t => t.Introduction).HasMaxLength(200);
            this.Property(t => t.Folder).HasMaxLength(100);
        }
    }

    public class VideoInfoMap : EntityTypeConfiguration<VideoInfo>
    {
        public VideoInfoMap()
        {
            this.Property(t => t.Title).HasMaxLength(50);
            this.Property(t => t.Source).HasMaxLength(100);
            this.Property(t => t.Introduction).HasMaxLength(100);
            this.Property(t => t.File).HasMaxLength(100);
            this.Property(t => t.Snapshot).HasMaxLength(100);
        }
    }
}
