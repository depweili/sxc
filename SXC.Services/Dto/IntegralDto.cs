using Newtonsoft.Json;
using SXC.Code.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SXC.Services.Dto
{
    public class IntegralActionResultDto
    {
        public string message { get; set; }

        public dynamic detail { get; set; }
    }

    public class IntegralChangeDto
    {
        public int points { get; set; }

        public int totalpoints { get; set; }

        public int currentpoints { get; set; }
    }

    public class UserIntegralDto
    {
        public Guid integralid { get; set; }

        public int totalpoints { get; set; }

        public int currentpoints { get; set; }

        public int totalexpense { get; set; }

        public string gradetitle { get; set; }
    }

    public class IntegralRecordDto
    {
        public string shortmark { get; set; }

        public int points { get; set; }

        [JsonConverter(typeof(CommonDateTimeConverter))]
        public DateTime? recordtime { get; set; }

        
    }


}
