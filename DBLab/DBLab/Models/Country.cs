using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBLab.Models
{
    public class Country
    {
        public virtual int id { get; set; }
        public virtual String nameCountry { get; set; }
        public virtual String region { get; set; }
        public virtual bool visa { get; set; }
        public virtual String nameCompany { get; set; }
        public virtual String embassy { get; set; }

        public virtual IList<Tour> tours { get; set; }
    }
}