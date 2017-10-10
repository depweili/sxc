using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SXC.Services.Dto
{
    public class VideoDto
    {

    }

    public class VideoSeriesDto
    {
        public int id { get; set; }

        public Guid videoseriesuid { get; set; }

        public string title { get; set; }

        public string cover { get; set; }

        public int total { get; set; }

        public List<VideoInfoDto> VideoList { get; set; }
    }

    public class VideoInfoDto
    {
        public int id { get; set; }

        public Guid videouid { get; set; }

        public string title { get; set; }

        public string introduction { get; set; }

        public string src { get; set; }

        public string snapshot { get; set; }

        public double length { get; set; }

        public int order { get; set; }
    }
}
