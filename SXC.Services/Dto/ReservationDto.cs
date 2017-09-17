using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SXC.Services.Dto
{
    public class ReservationDto
    {
        public int id { get; set; }

        public Guid authid { get; set; }

        public string name { get; set; }

        public string mobilephone { get; set; }

        public string areainfo { get; set; }
        public string address { get; set; }

        public string purpose { get; set; }

        public string memo { get; set; }

        public string courseids { get; set; }
    }
}
