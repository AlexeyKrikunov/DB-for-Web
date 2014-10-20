using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBLab.Models
{
    public class Hotel
    {
        public virtual int id { get; set; }
        public virtual String name { get; set; }
        public virtual int category { get; set; }
        public virtual String city { get; set; }
        public virtual String address { get; set; }
        public virtual bool language { get; set; }

        public virtual IList<PlacesHotel> places { get; set; }
    }
}