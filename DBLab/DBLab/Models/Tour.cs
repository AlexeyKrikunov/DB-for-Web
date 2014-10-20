using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBLab.Models
{
    public class Tour
    {
        public virtual int id { get; set; }
        public virtual String type { get; set; }
        public virtual String rateType { get; set; }
        public virtual int numberNights { get; set; }
        public virtual int tourPrice { get; set; }
        public virtual String healthInsurance { get; set; }
        public virtual Country country { get; set; }

        public virtual IList<PlacesHotel> hotels { get; set; }
        public virtual IList<SeatsTransport> transports { get; set; }
        public virtual IList<GroupTourists> groupTourists { get; set; }
    }
}