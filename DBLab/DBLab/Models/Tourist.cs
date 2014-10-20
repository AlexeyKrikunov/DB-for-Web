using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBLab.Models
{
    public class Tourist
    {
        public virtual int id { get; set; }
        public virtual String surname { get; set; }
        public virtual String name { get; set; }
        public virtual String patronymic { get; set; }
        public virtual String passportData { get; set; }
        public virtual String city { get; set; }
        public virtual String phoneNumber { get; set; }
        public virtual String address { get; set; }

        public virtual IList<GroupTourists> group { get; set; }
    }
}