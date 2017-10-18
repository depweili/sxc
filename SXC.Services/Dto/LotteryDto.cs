using Newtonsoft.Json;
using SXC.Code.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SXC.Services.Dto
{

    public class LotteryDto
    {
        public int id { get; set; }

        public int chance { get; set; }

        [JsonConverter(typeof(CommonDateTimeConverter))]
        public DateTime? begintime  { get; set; }

        [JsonConverter(typeof(CommonDateTimeConverter))]
        public DateTime? endtime { get; set; }

        public bool isvalid { get; set; }

        public List<PrizeDto> prizes { get; set; }

    }

    public class PrizeDto
    {
        public int id { get; set; }

        public string imgurl { get; set; }

        public string name { get; set; }
    }

    public class WinPrizeDto
    {
        public int remainingchance { get; set; }

        public PrizeDto prize { get; set; }

        public string message { get; set; }
    }


}
