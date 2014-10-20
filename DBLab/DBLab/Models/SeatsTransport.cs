using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBLab.Models
{
    public class SeatsTransport
    {
        public virtual int id { get; set; }
        public virtual DateTime date { get; set; }
        public virtual String cityOrigin { get; set; }
        public virtual String cityArrival { get; set; }
        public virtual int seats { get; set; }
        public virtual String Class { get; set; }
        public virtual int tiketPrice { get; set; }
        public virtual int number { get; set; }
        public virtual Tour tour { get; set; }
        public virtual Transport transport { get; set; }

    }
}