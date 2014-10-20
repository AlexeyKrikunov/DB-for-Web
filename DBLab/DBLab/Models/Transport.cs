using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBLab.Models
{
    public class Transport
    {
        public virtual int id { get; set; }
        public virtual String name { get; set; }
        public virtual String typeTransport { get; set; }
        public virtual String address { get; set; }
        public virtual int licenseNumber { get; set; }
        public virtual String country { get; set; }

        public virtual IList<SeatsTransport> seats { get; set; }
    }
}