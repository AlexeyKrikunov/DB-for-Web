using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBLab.Models
{
    public class GroupTourists
    {
        public virtual int id { get; set; }
        public virtual String surnameGuide { get; set; }
        public virtual String nameGuide { get; set; }
        public virtual String patronymicGuide { get; set; }

        public virtual Tourist tourist { get; set; }
        public virtual Tour tour { get; set; }
    }
}