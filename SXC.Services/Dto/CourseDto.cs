using Newtonsoft.Json;
using SXC.Code.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SXC.Services.Dto
{
    public class CourseDto
    {
        public int id { get; set; }

        public string name { get; set; }

        public string desc { get; set; }

        [JsonConverter(typeof(DecimalDigitsConverter))]
        public decimal? price { get; set; }

        public double? period { get; set; }

        public bool? hasvideo { get; set; }

        public bool? hasfreevideo { get; set; }

        public string picurl { get; set; }

        public int? articleid { get; set; }
    }
}
