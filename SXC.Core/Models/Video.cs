using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SXC.Core.Models
{
    public class VideoSeries
    {

        public VideoSeries()
        {
            IsValid = true;
            CreateTime = DateTime.Now;
            VideoSeriesUID = Guid.NewGuid();
        }
        public int ID { get; set; }

        public Guid VideoSeriesUID { get; set; }

        public string Title { get; set; }

        public string Cover { get; set; }

        public string Folder { get; set; }

        public string Introduction { get; set; }

        public int? Total { get; set; }

        public bool? IsValid { get; set; }

        public DateTime? CreateTime { get; set; }

        public virtual ICollection<VideoInfo> VideoInfos { get; set; }

    }

    public class VideoInfo
    {
        public VideoInfo()
        {
            IsValid = true;
            CreateTime = DateTime.Now;
            VideoUID = Guid.NewGuid();
        }
        public int ID { get; set; }

        public Guid VideoUID { get; set; }

        public string Title { get; set; }

        public string Introduction { get; set; }

        public string File { get; set; }

        public string Source { get; set; }

        public string Snapshot { get; set; }

        public double? Length { get; set; }

        public int Order { get; set; }

        public bool? IsValid { get; set; }

        public DateTime? CreateTime { get; set; }

        public int VideoSeriesID { get; set; }
        public virtual VideoSeries VideoSeries { get; set; }

    }

    public class CommodityVideoSeries
    {
        public int ID { get; set; }

        public int CommodityID { get; set; }
        public virtual Commodity Commodity { get; set; }

        public int VideoSeriesID { get; set; }
        public virtual VideoSeries VideoSeries { get; set; }

        public bool? IsValid { get; set; }

        public DateTime? CreateTime { get; set; }


    }

    public class UserVideoSeries
    {
        public int ID { get; set; }

        public int UserID { get; set; }
        public virtual User User { get; set; }

        public int VideoSeriesID { get; set; }
        public virtual VideoSeries VideoSeries { get; set; }




    }
}
