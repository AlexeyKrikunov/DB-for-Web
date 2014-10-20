using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBLab.Models
{
    public class Application
    {
        public virtual int id { get; set; }
        public virtual String idCountry { get; set; }
        public virtual String idCityArrive { get; set; }
        public virtual String idTourType { get; set; }
        public virtual String idCityDeparture { get; set; }
        public virtual String idTypeTariff { get; set; }
        public virtual String idFrom { get; set; }
        public virtual String idTo { get; set; }
        public virtual String idTypeFood { get; set; }
        public virtual String idTypeRoom { get; set; }
        public virtual String idTypeHotel { get; set; }
        public virtual String idHotel { get; set; }
        public virtual String idStartDay { get; set; }
        public virtual String idStartMonth { get; set; }
        public virtual String idStartYear { get; set; }
        public virtual String idStopDay { get; set; }
        public virtual String idStopMonth { get; set; }
        public virtual String idStopYear { get; set; }
        public virtual String idNumberTourists { get; set; }
        public virtual String Name { get; set; }
        public virtual String Surname { get; set; }
        public virtual String Patronymic { get; set; }
        public virtual String PhoneNumber { get; set; }
        public virtual String Email { get; set; }
        public virtual int idSearch { get; set; }
    }
}