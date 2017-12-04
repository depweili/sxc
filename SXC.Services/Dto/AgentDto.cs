using Newtonsoft.Json;
using SXC.Code.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SXC.Services.Dto
{
    public class AgentDto
    {
        public string code { get; set; }

        public int type { get; set; }

        public int level { get; set; }

        public int state { get; set; }

        [JsonConverter(typeof(DecimalDigitsConverter))]
        public decimal commission { get; set; }

        public Nullable<bool> isvalid { get; set; }
    }

    public class BindSupAgentDto
    {
        public string agentcode { get; set; }

        public Guid authid { get; set; }
    }

    public class CommissionRecordDto
    {
        public int ID { get; set; }

        public decimal Commission { get; set; }

        public string PayUser { get; set; }

        [JsonConverter(typeof(CommonDateTimeConverter))]
        public DateTime CreateTime { get; set; }

        public string Memo { get; set; }

    }
}
