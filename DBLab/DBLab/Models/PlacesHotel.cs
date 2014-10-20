using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace DBLab.Models
{
    public class PlacesHotel
    {
        public virtual int id { get; set; }
        public virtual DateTime date { get; set; }
        public virtual int places { get; set; }
        public virtual String typeEat { get; set; }
        public virtual String typeRoom { get; set; }
        public virtual bool animals { get; set; }
        public virtual int roomPrice { get; set; }
        public virtual Tour tour { get; set; }
        public virtual Hotel hotel { get; set; }


    }
}