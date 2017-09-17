using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SXC.Core.Models
{
    public class Reservation
    {
        public Reservation()
        {
            CreateTime = DateTime.Now;
        }

        public int ID { get; set; }

        public string Name { get; set; }

        public string MobilePhone { get; set; }

        public string AreaInfo { get; set; }

        public string Address { get; set; }

        public string Purpose { get; set; }

        public string Memo { get; set; }

        public Nullable<DateTime> ReservedDate { get; set; }

        public int State { get; set; }

        public string ProcessDetail { get; set; }

        public DateTime CreateTime { get; set; }


        //public int PurposeID { get; set; }
        //public virtual LearnPurpose Purpose { get; set; }

        public int UserID { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<ReservationCourse> ReservationCourses { get; set; }
    }

    public class ReservationCourse
    {
        public int ID { get; set; }

        public int ReservationID { get; set; }

        public virtual Reservation Reservation { get; set; }

        public int CourseID { get; set; }

        public virtual Course Course { get; set; }
    }

    public class LearnPurpose
    {
        public int ID { get; set; }

        public string Purpose { get; set; }
    }
}
